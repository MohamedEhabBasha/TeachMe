namespace Domain;

public class User : Aggregate<UserId>
{
    public string Name { get; private set; } = default!;
    public string UserName { get; private set; } = default!;
    public UserRole Role { get; private set; } = default!;
    public static User CreateStudent(Guid id, string name, string userName)
    {
        // Domain Validation
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);

        return new User(id, name, userName, UserRole.Student);
    }
    public static User CreateInstructor(Guid id, string name, string userName)
    {
        // Domain Validation
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);

        return new User(id, name, userName, UserRole.Instructor);
    }
    public static User CreateAdministrator(string name, string userName)
    {
        // Domain Validation
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);

        return new User(Guid.NewGuid(), name, userName, UserRole.Administrator);
    }
    public User() { }
    private User(Guid id, string name, string userName, UserRole userRole)
    {
        Id = new UserId(id);
        Name = name;
        UserName = userName;
        Role = userRole;
    }
}
