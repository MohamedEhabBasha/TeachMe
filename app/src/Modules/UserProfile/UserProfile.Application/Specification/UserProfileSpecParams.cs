using System.Linq.Expressions;

namespace UserProfile.Application.Specification;

public record UserProfileSpecParams
    (
        bool ApplyPagination,
        bool ApplyProjection,
        string[] Includes, 
        Expression<Func<UserProfile.Domain.UserProfiles.UserProfile, bool>>? Criteria,
        Expression<Func<UserProfile.Domain.UserProfiles.UserProfile, object>>? OrderByDescending = null
    ) : PaginationRequest;
