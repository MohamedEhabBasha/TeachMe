using System.Linq.Expressions;

namespace Application.Specification;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
    IQueryable<T> ApplyCriteria(IQueryable<T> query);
}
public interface ISpecification<T, TResult> : ISpecification<T>
{
    //Expression<Func<T, TResult>>? Select { get; }
    bool IsProjected { get; }
}