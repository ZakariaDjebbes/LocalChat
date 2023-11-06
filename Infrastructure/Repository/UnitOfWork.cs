using System.Collections;
using Core;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly LocalChatDbContext _dbContext;
    private readonly Hashtable _repositories;

    public UnitOfWork(IDbContextFactory<LocalChatDbContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
        _repositories = new Hashtable();
    }

    public IRepository<T> GetRepository<T>() where T : class, IEntity
    {
        var type = typeof(T);
        if (_repositories.ContainsKey(type)) return (IRepository<T>)_repositories[type];
        var repositoryType = typeof(Repository<>);
        var repositoryInstance = Activator.CreateInstance(
            repositoryType.MakeGenericType(typeof(T)),
            _dbContext);
        _repositories.Add(type, repositoryInstance);

        return (IRepository<T>) _repositories[type];
    }

    public int Commit()
        => _dbContext.SaveChanges();

    public void Rollback()
        => _dbContext.Dispose();
}