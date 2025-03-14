using Application.Specification;

namespace UserProfile.Application.Specification;

public class UserProfilesSpecification : BaseSpecification<Userprofile>
{
    public UserProfilesSpecification(UserProfileSpecParams specParams) : base(specParams.Criteria)
    {
        if (specParams.OrderByDescending is not null)
        {
            AddOrderByDescending(specParams.OrderByDescending);
        }
        if (specParams.ApplyPagination)
        {
            ApplyPaging(specParams.PageSize * (specParams.PageNumber - 1), specParams.PageSize);
        }

        if (specParams.Includes != null)
        {
            if (specParams.Includes.Contains("category") == true)
                AddInclude(u => u.Categories);

            if (specParams.Includes.Contains("userFollows") == true)
                AddInclude(u => u.UserFollowers);
        }
    }
}
public class UserProfilesSpecification<TResult> : BaseSpecification<Userprofile, TResult>
{
    public UserProfilesSpecification(UserProfileSpecParams specParams) : base(specParams.Criteria!)
    {
        if (specParams.ApplyPagination)
            ApplyPaging(specParams.PageSize * (specParams.PageNumber - 1), specParams.PageSize);

        if (specParams.ApplyProjection)
            ApplyProjection();

        if (specParams.Includes != null)
        {
            if (specParams.Includes.Contains("category") == true)
                AddInclude(u => u.Categories);

            if (specParams.Includes.Contains("userFollows") == true)
                AddInclude(u => u.UserFollowers);
        }
    }
}
