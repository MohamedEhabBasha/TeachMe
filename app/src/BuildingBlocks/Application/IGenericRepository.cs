using Application.Specification;

namespace Application;

public interface IGenericRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<int> CountAsync(ISpecification<T> spec);
    Task<T?> GetEntityWithSpec(ISpecification<T> spec, CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken);
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec, CancellationToken cancellationToken);
}
