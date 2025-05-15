using Infrastructure.MS_AuthenticationAutorization.Data;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> Exist(Guid id, string sql, CancellationToken cancellationToken = default)
    {
        using var connection = authDbContext.Database.GetDbConnection();

        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = sql;

        var parameter = command.CreateParameter();
        parameter.ParameterName = "@Id";

        parameter.Value = id;
        command.Parameters.Add(parameter);

        var result = await command.ExecuteScalarAsync(cancellationToken);
        return Convert.ToInt32(result) > 0;
    }
}
