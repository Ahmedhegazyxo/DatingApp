using System.Linq.Expressions;
using Api.Helpers;

public interface IBaseRepository<TEntity,TId> where TEntity : BaseEntity<TId> where TId : IEquatable<TId>
{
    Task<TId> CreateAsync(TEntity entity, CancellationToken cancellationToken = default!);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default!);
    Task<TId> DeleteAsync(TId id, CancellationToken cancellationToken = default!);
    Task<bool> ExistsAsync(Expression<Func<TEntity,bool>> predicate, CancellationToken cancellationToken = default!);
    Task<TEntity?> ReadyByIdAsync(TId id, CancellationToken cancellationToken = default!, bool throwIfNull = true);
    Task<TEntity?> ReadyByIdAsyncAsNoTracking(TId id, CancellationToken cancellationToken = default!, bool throwIfNull = true);
    Task<List<TEntity>> Read(PaginationFilter? paginationFilter, CancellationToken cancellationToken = default!, Expression<Func<TEntity , bool>>? predicate = null);
    Task<List<TResult>> ReadAsResult<TResult>(PaginationFilter? paginationFilter, Expression<Func<TEntity , TResult>> resultExpression , CancellationToken cancellationToken = default!, Expression<Func<TEntity , bool>>? predicate = null);
    Task<List<TEntity>> ReadAsNoTracking(PaginationFilter? paginationFilter, CancellationToken cancellationToken = default!, Expression<Func<TEntity , bool>>? predicate = null, CancellationToken ct = default);
    Task<List<TResult>> ReadAsResultAsNoTracking<TResult>(PaginationFilter? paginationFilter,  Expression<Func<TEntity , TResult>> resultExpresion, CancellationToken cancellationToken = default!, Expression<Func<TEntity , bool>>? predicate = null);
}
public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
     Task<int> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<int> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<TEntity?> ReadyByIdAsync(int id, CancellationToken cancellationToken, bool throwIfNull = true);
    Task<TEntity?> ReadyByIdAsyncAsNoTracking(int id, CancellationToken cancellationToken, bool throwIfNull = true);
    Task<List<TEntity>> Read(PaginationFilter? paginationFilter, CancellationToken cancellationToken);
    Task<List<TEntity>> ReadAsNoTracking(PaginationFilter? paginationFilter, CancellationToken cancellationToken);
}