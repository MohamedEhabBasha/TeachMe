namespace UserProfile.Domain.UserProfiles;

public class Category : Entity<UserProfileId>
{
    public Category() 
    {
        // Ef Core
    }
    internal Category(UserProfileId profileId, string name)
    {
        Id = profileId;
        Name = name;
    }
    public string Name { get; private set; } = default!;
}
