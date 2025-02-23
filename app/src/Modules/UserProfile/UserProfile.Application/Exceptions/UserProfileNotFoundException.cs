using Application.Exceptions;

namespace UserProfile.Application.Exceptions;

public class UserProfileNotFoundException(Guid id) : NotFoundException("UserProfile", id)
{
}
