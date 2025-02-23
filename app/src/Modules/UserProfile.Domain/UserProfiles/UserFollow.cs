namespace UserProfile.Domain.UserProfiles;

public class UserFollow : Entity<UserProfileId>
{
    public UserFollow()
    {
        // Ef Core
    }
    internal UserFollow(UserProfileId profileId, UserProfileId studnetId)
    {
        Id = profileId;
        StudentId = studnetId;
    }
    public UserProfileId StudentId { get; private set; } = default!;
}
