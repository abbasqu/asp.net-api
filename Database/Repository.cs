using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace test_api_dotnet.Database;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly MyDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(MyDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).FirstAsync();
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> ListAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public IQueryable<T> AsQueryable()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }
}
