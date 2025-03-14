namespace UserProfile.Domain.UserProfiles;

public class UserProfile : Aggregate<UserProfileId>
{
    private readonly List<Category> _categories = [];
    public IReadOnlyList<Category> Categories => _categories.AsReadOnly();

    private readonly List<UserFollow> _userFollowers = [];
    public IReadOnlyList<UserFollow> UserFollowers => _userFollowers.AsReadOnly();

    public string Name { get; private set; } = default!;
    public string Role { get; private set; } = default!;
    public string? Introduction { get; private set; }
    public string? Description { get; private set; }
    public Photo Photo { get; private set; } = default!;
    
    public static UserProfile Create(Guid id, string name, string role)
    {
        var userProfile = new UserProfile
        {
            Id = new UserProfileId(id),
            Name = name,
            Role = role,      
            Photo = new Photo()    // EF Core does not support null complex properties
        };

        return userProfile;
    }

    public void Update(string intro, string description)
    {
        Introduction = intro;
        Description = description;
    }
    public void AddCategory(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        var category = new Category(Id, name);
        _categories.Add(category);
    }
    public void RemoveCategory(string name)
    {
        var category = _categories.FirstOrDefault(c => c.Name == name);

        if (category != null)
        {
            _categories.Remove(category);
        }
    }
    public void UpdatePhoto(Photo photo)
    {
        Photo = photo;
    }
    public void Follow(UserProfileId studentId)
    {
        var userFollower = new UserFollow(Id, studentId);
        _userFollowers.Add(userFollower);
    }
    public void UnFollow(UserProfileId studentId)
    {
        var userFollower = _userFollowers.FirstOrDefault(c => c.StudentId == studentId);

        if (userFollower != null)
        {
            _userFollowers.Remove(userFollower);
        }
    }
}
