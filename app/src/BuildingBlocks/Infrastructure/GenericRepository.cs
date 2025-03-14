using Application;
using Application.Specification;
using AutoMapper;
using Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class GenericRepository<T>(DbContext context, IMapper mapper)
    : IGenericRepository<T> where T : class
{
    public async Task AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var query = context.Set<T>().AsQueryable();

        query = spec.ApplyCriteria(query);

        return await query.CountAsync();
    }
    public async Task<T?> GetEntityWithSpec(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken); ;
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec, CancellationToken cancellationToken)
    {
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }
    private IQueryable<T> ApplySpecification<T>(ISpecification<T> spec) where T : class
    {
        return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
    }
    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec, mapper);
    }
}
