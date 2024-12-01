using System.Linq.Expressions;

namespace test_api_dotnet.Database;

public interface IRepository<T> where T : class
{
    Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}
