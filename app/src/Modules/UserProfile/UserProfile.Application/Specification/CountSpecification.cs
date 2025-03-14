using Application.Specification;
using System.Linq.Expressions;
using Userprofile = UserProfile.Domain.UserProfiles.UserProfile;

namespace UserProfile.Application.Specification;

public class CountSpecification(Expression<Func<Userprofile, bool>>? criteria) : BaseSpecification<Userprofile>(criteria)
{
}
