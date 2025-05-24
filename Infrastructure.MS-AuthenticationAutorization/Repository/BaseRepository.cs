using Infrastructure.MS_AuthenticationAutorization.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.MS_AuthenticationAutorization.Repository;

public class BaseRepository<T> where T : class
{
    protected readonly AuthDbContext authDbContext;
    private readonly DbSet<T> _dbSet;
    public BaseRepository(AuthDbContext authDbContext)
    {
        this.authDbContext = authDbContext;
        _dbSet = authDbContext.Set<T>();
    }

    public async Task<bool> GenericAddAsync(T obj, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbSet.AddAsync(obj, cancellationToken);
            var response = await authDbContext.SaveChangesAsync(cancellationToken);
            return response > 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> GenericUpdateAsync(T obj, CancellationToken cancellationToken = default)
    {
        try
        {
            _dbSet.Update(obj);
            var response = await authDbContext.SaveChangesAsync(cancellationToken);
            return response > 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> GenericUpdatecompositekeyAsync(T obj, CancellationToken cancellationToken = default)
    {
        try
        {
            return await authDbContext.SaveChangesAsync(cancellationToken) > 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }
}
