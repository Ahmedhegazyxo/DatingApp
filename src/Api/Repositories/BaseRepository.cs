using System.Linq.Expressions;
using Api.Helpers;

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
    public async Task<TEntity> UpdateAsync(TEntity entity,
     CancellationToken cancellationToken = default!)
    {
        TEntity oldEntity = await _context.Set<TEntity>().FirstAsync(e => e.Id.Equals(entity.Id), cancellationToken);
        oldEntity = entity;
        _context.Set<TEntity>().Update(oldEntity);
        await _context.SaveChangesAsync(cancellationToken);
        return oldEntity;
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
        return await _context.Set<TEntity>().AsNoTracking().AnyAsync(predicate, cancellationToken);
    }

    public async Task<TEntity?> ReadyByIdAsync(TId id, CancellationToken cancellationToken = default, bool throwIfNull = true)
    {
        if (!throwIfNull)
            return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
        else
            return await _context.Set<TEntity>().FirstAsync(e => e.Id.Equals(id), cancellationToken);
    }

    public async Task<TEntity?> ReadyByIdAsyncAsNoTracking(TId id, CancellationToken cancellationToken = default, bool throwIfNull = true)
    {
        if (!throwIfNull)
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
        else
            return await _context.Set<TEntity>().AsNoTracking().FirstAsync(e => e.Id.Equals(id), cancellationToken);
    }

    public async Task<List<TEntity>> Read(CancellationToken cancellationToken = default, Expression<Func<TEntity, bool>>? predicate = null)
    {
        predicate ??= e => true;
        return await _context.Set<TEntity>()
        .Where(predicate)
        .ToListAsync(cancellationToken);
    }
    public async Task<PaginatedResult<TEntity>> Read(PaginationFilter paginationFilter, CancellationToken cancellationToken = default, Expression<Func<TEntity, bool>>? predicate = null)
    {
        predicate ??= e => true;
        IQueryable<TEntity> query = _context.Set<TEntity>()
        .AsNoTracking()
        .Where(predicate);
        return new PaginatedResult<TEntity>
        {
            PageNumber = paginationFilter.PageNumber,
            PageSize = paginationFilter.PageSize,
            TotalCount = await query.CountAsync(),
            Body = await query
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync(cancellationToken)
        };
    }

    public async Task<List<TEntity>> ReadAsNoTracking(CancellationToken cancellationToken = default, Expression<Func<TEntity, bool>>? predicate = null, CancellationToken ct = default)
    {
        predicate ??= e => true;
        return await _context.Set<TEntity>()
        .AsNoTracking()
        .Where(predicate)
        .ToListAsync(cancellationToken);
    }
    public async Task<PaginatedResult<TEntity>> ReadAsNoTracking(PaginationFilter paginationFilter, CancellationToken cancellationToken = default, Expression<Func<TEntity, bool>>? predicate = null, CancellationToken ct = default)
    {
        predicate ??= e => true;
        var query = _context.Set<TEntity>()
       .AsNoTracking()
       .Where(predicate);

        return new PaginatedResult<TEntity>
        {
            TotalCount = await query.CountAsync(cancellationToken),
            PageNumber = paginationFilter.PageNumber,
            PageSize = paginationFilter.PageSize,
            Body = await query.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize).ToListAsync(cancellationToken)
        };
    }

    public async Task<PaginatedResult<TResult>> ReadAsResult<TResult>(PaginationFilter paginationFilter, Expression<Func<TEntity, TResult>> resultExpression, CancellationToken cancellationToken = default, Expression<Func<TEntity, bool>>? predicate = null) where TResult : class
    {
        predicate ??= e => true;
        var query = _context.Set<TEntity>()
        .AsNoTracking()
        .Where(predicate)
        .Select(resultExpression);
        return new PaginatedResult<TResult>
        {
            PageNumber = paginationFilter.PageNumber,
            PageSize = paginationFilter.PageSize,
            TotalCount = await query.CountAsync(cancellationToken),
            Body = await query
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync(cancellationToken),
        };

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

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().AsNoTracking().AnyAsync(predicate, cancellationToken);
    }

    public Task<List<TEntity>> Read(PaginationFilter? paginationFilter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> ReadAsNoTracking(PaginationFilter? paginationFilter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<TResult>> ReadAsResult<TResult>(PaginationFilter? paginationFilter, Expression<Func<TEntity, TResult>> selector, Func<TEntity, bool>? predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<TResult>> ReadAsResultAsNoTracking<TResult>(PaginationFilter? paginationFilter, Expression<Func<TEntity, TResult>> selector, Func<TEntity, bool>? predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> ReadyByIdAsync(int id, CancellationToken cancellationToken, bool throwIfNull = true)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> ReadyByIdAsyncAsNoTracking(int id, CancellationToken cancellationToken, bool throwIfNull = true)
    {
        throw new NotImplementedException();
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