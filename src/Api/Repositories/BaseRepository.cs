using System.Linq.Expressions;
using Api.Helpers;
using SQLitePCL;

namespace Api.Repositories;

public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId> where TEntity : BaseEntity<TId> where TId : IEquatable<TId>
{
    private readonly ApplicationDbContext _context;
    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<TId> CreateAsync(TEntity entity,
     CancellationToken cancellationToken = default!)
    {
        EntityEntry<TEntity> entry = await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }
    public async Task<TId> DeleteAsync(TId id,
     CancellationToken cancellationToken = default!)
    {
        var entity = await _context.Set<TEntity>().FirstAsync(e => e.Id.Equals(id), cancellationToken);
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return id;
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().AsNoTracking().AnyAsync(predicate,cancellationToken);
    }

    public async Task<List<TEntity>> Read(PaginationFilter? paginationFilter,
     CancellationToken cancellationToken,
      Expression<Func<TEntity, bool>>? predicate = null)
    {

        predicate ??= e => true;
        if (paginationFilter is not null)
            return await _context.Set<TEntity>()
            .Where(predicate)
            .Skip((paginationFilter.PageSize - paginationFilter.PageSize) * paginationFilter.PageSize)
            .Take(paginationFilter.PageNumber)
            .ToListAsync(cancellationToken);
        else
        {
            return await _context.Set<TEntity>()
            .Where(predicate)
            .ToListAsync(cancellationToken);
        }
    }

    public async Task<List<TEntity>> ReadAsNoTracking(PaginationFilter?
     paginationFilter, CancellationToken cancellationToken = default!,
      Expression<Func<TEntity, bool>>? predicate = null)
    {
        predicate ??= e => true;
        if (paginationFilter is not null)
            return await _context.Set<TEntity>()
            .Where(predicate)
            .Skip((paginationFilter.PageSize - paginationFilter.PageSize) * paginationFilter.PageSize)
            .Take(paginationFilter.PageNumber).AsNoTracking().ToListAsync(cancellationToken);
        else
        {
            return await _context.Set<TEntity>().AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken);
        }
    }

    public Task<List<TEntity>> ReadAsNoTracking(PaginationFilter? paginationFilter, CancellationToken cancellationToken = default, Expression<Func<TEntity, bool>>? predicate = null, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TResult>> ReadAsResult<TResult>(PaginationFilter? paginationFilter,
        Expression<Func<TEntity, TResult>> resultExpression,
        CancellationToken cancellationToken = default!,
        Expression<Func<TEntity, bool>>? predicate = default)
    {
        predicate ??= e => true;
        if (paginationFilter is not null)
        {
            return await _context.Set<TEntity>()
            .Where(predicate)
            .Skip((paginationFilter.PageNumber - paginationFilter.PageNumber) * paginationFilter.PageNumber)
            .Take(paginationFilter.PageSize)
            .Select(resultExpression)
            .ToListAsync(cancellationToken);
        }
        else
        {
            return await _context.Set<TEntity>()
            .Where(predicate)
            .Select(resultExpression).ToListAsync(cancellationToken);
        }
    }

    public async Task<List<TResult>> ReadAsResultAsNoTracking<TResult>(PaginationFilter? paginationFilter,
      Expression<Func<TEntity, TResult>> resultExpresion,
     CancellationToken cancellationToken = default!,
       Expression<Func<TEntity, bool>>? predicate = null)
    {
        predicate ??= e => true;
        if (paginationFilter is not null)
        {
            return await _context.Set<TEntity>()
            .AsNoTracking()
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageNumber)
            .Take(paginationFilter.PageSize)
            .Where(predicate)
            .Select(resultExpresion)
            .ToListAsync(cancellationToken);
        }
        else
        {
            return await _context.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .Select(resultExpresion).ToListAsync(cancellationToken);
        }
    }

    public async Task<TEntity?> ReadyByIdAsync(TId id,
     CancellationToken cancellationToken = default!,
      bool throwIfNull = true)
    {
        if (throwIfNull)
            return await _context.Set<TEntity>().FirstAsync(e => e.Id.Equals(id), cancellationToken);
        else
            return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
    }

    public async Task<TEntity?> ReadyByIdAsyncAsNoTracking(TId id,
     CancellationToken cancellationToken = default!,
      bool throwIfNull = true)
    {
        if (throwIfNull)
            return await _context.Set<TEntity>().AsNoTracking().FirstAsync(e => e.Id.Equals(id), cancellationToken);
        else
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
    }

    public async Task<TId> UpdateAsync(TEntity entity,
     CancellationToken cancellationToken = default!)
    {
        TEntity oldEntity = await _context.Set<TEntity>().FirstAsync(e => e.Id.Equals(entity.Id), cancellationToken);
        oldEntity = entity;
        await Task.Run(async () => _context.Set<TEntity>().Update(oldEntity));
        await _context.SaveChangesAsync(cancellationToken);
        return oldEntity.Id;
    }
}
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _context;
    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        EntityEntry<TEntity> entry = await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }
    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        TEntity entity = await _context.Set<TEntity>().FirstAsync(e => e.Id.Equals(id), cancellationToken);
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return id;
    }

    public async Task<List<TEntity>> Read(PaginationFilter? paginationFilter, CancellationToken cancellationToken)
    {
        if (paginationFilter is not null)
            return await _context.Set<TEntity>()
            .Skip((paginationFilter.PageSize - paginationFilter.PageSize) * paginationFilter.PageSize)
            .Take(paginationFilter.PageNumber).ToListAsync(cancellationToken);
        else
        {
            return await _context.Set<TEntity>()
            .ToListAsync(cancellationToken);
        }
    }

    public async Task<List<TEntity>> ReadAsNoTracking(PaginationFilter? paginationFilter, CancellationToken cancellationToken)
    {
        if (paginationFilter is not null)
            return await _context.Set<TEntity>()
            .Skip((paginationFilter.PageSize - paginationFilter.PageSize) * paginationFilter.PageSize)
            .Take(paginationFilter.PageNumber).AsNoTracking().ToListAsync(cancellationToken);
        else
        {
            return await _context.Set<TEntity>().AsNoTracking()
            .ToListAsync(cancellationToken);
        }
    }

    public async Task<TEntity?> ReadyByIdAsync(int id, CancellationToken cancellationToken, bool throwIfNull = true)
    {
        if (throwIfNull)
            return await _context.Set<TEntity>().FirstAsync(e => e.Id.Equals(id), cancellationToken);
        else
            return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
    }

    public async Task<TEntity?> ReadyByIdAsyncAsNoTracking(int id, CancellationToken cancellationToken, bool throwIfNull = true)
    {
        if (throwIfNull)
            return await _context.Set<TEntity>().AsNoTracking().FirstAsync(e => e.Id.Equals(id), cancellationToken);
        else
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
    }

    public async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        TEntity oldEntity = await _context.Set<TEntity>().FirstAsync(e => e.Id.Equals(entity.Id));
        oldEntity = entity;
        _context.Set<TEntity>().Update(oldEntity);
        await _context.SaveChangesAsync(cancellationToken);
        return oldEntity.Id;
    }
}