using System.Linq.Expressions;

namespace test_api_dotnet.Database;

public interface IRepository<T> where T : class
{
    Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> AsQueryable();
    Task<IEnumerable<T>> ListAsync();
    Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}
