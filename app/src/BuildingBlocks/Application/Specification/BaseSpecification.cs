using System.Linq.Expressions;

namespace Application.Specification;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria => criteria;
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public List<Expression<Func<T, object>>> Includes { get; } = [];
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }
    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if (Criteria != null)
        {
            query = query.Where(Criteria);
        }

        return query;
    }
    protected void AddInclude(Expression<Func<T, object>> includeExpressions)
    {
        Includes.Add(includeExpressions);
    }
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}
public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria)
    : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    protected BaseSpecification() : this(null!) { }

    public bool IsProjected { get; private set; }

    protected void ApplyProjection()
    {
        IsProjected = true;
    }
}