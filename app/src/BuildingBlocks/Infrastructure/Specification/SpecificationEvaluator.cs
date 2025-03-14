using Application.Specification;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Specification;

public class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }
        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
    public static IQueryable<TResult> GetQuery<TResult>(IQueryable<T> query, ISpecification<T, TResult> spec, IMapper mapper)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        var selectQuery = query as IQueryable<TResult>;

        if (spec.IsProjected)
        {
            selectQuery = query.ProjectTo<TResult>(mapper.ConfigurationProvider);
        }

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return selectQuery ?? query.Cast<TResult>();
    }
}
