using Application;

namespace UserProfile.Application.Contracts;

public interface IUserProfileUnitOfWork : IUnitOfWork
{
    IUserProfileRepository UserProfileRepository { get; }
}
