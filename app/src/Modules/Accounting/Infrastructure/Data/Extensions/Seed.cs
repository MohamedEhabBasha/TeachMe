namespace Accounting.Infrastructure.Data.Extensions;

public class Seed
{
    public static async Task SeedRoles(RoleManager<AppRole> roleManager)
    {
        if (await roleManager.Roles.AnyAsync()) return;

        var roles = new List<AppRole>
        {
            new() {Name = "Student"},
            new() {Name = "Instructor"},
            new() {Name = "Administrator"},
        };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }
    }
}
