using Core;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    private readonly LocalChatDbContext _dbContext;
    
    public Repository(LocalChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Add(T entity) => _dbContext.Set<T>().Add(entity);

    public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

    public T GetById(Guid id) => _dbContext.Set<T>().Find(id);

    public IEnumerable<T> GetAll() => _dbContext.Set<T>().AsNoTracking().ToList();
    
    public void Update(T entity)
    {
        _dbContext.Set<T>().Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }
    
    public int Commit() => _dbContext.SaveChanges();
}