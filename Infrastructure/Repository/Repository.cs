using System.Linq.Expressions;
using Core.Model;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    private readonly LocalChatDbContext _dbContext;

    public Repository(IDbContextFactory<LocalChatDbContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }

    public void Add(T entity)
    {
        _dbContext.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public T GetById(Guid id)
    {
        return _dbContext.Set<T>().Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _dbContext.Set<T>().AsNoTracking().ToList();
    }

    public IEnumerable<T> GetAllWithInclude(params Expression<Func<T, object>>[] includeProperties)
    {
        var query = _dbContext.Set<T>().AsNoTracking();
        query = includeProperties
            .Where(includeProperty => includeProperty.Body is MemberExpression)
            .Aggregate(query,
                (current, includeProperty)
                    => current.Include(includeProperty));
        return query.ToList();
    }
    
    public IEnumerable<T> GetAllWithInclude(params string[] includeProperties)
    {
        var query = _dbContext.Set<T>().AsNoTracking();
        query = includeProperties
            .Where(includeProperty => !string.IsNullOrEmpty(includeProperty))
            .Aggregate(query,
                (current, includeProperty)
                    => current.Include(includeProperty));
        return query.ToList();
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public int Commit()
    {
        return _dbContext.SaveChanges();
    }
}