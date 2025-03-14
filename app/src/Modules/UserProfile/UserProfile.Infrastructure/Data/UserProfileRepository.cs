using AutoMapper;
using Infrastructure;
using Userprofile = UserProfile.Domain.UserProfiles.UserProfile;

namespace UserProfile.Infrastructure.Data;

public class UserProfileRepository(UserProfileContext context, IMapper mapper) 
    : GenericRepository<Userprofile>(context, mapper), IUserProfileRepository
{
}
