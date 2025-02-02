namespace Domain;

public record UserRole : ValueObject
{
    public static UserRole Student => new(nameof(Student));
    public static UserRole Instructor => new(nameof(Instructor));
    public static UserRole Administrator => new(nameof(Administrator));
    public string Name { get; }

    public UserRole(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
    }
}
