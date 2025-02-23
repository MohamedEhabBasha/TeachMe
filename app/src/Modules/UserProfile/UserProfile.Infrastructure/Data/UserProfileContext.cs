using System.Reflection;

namespace UserProfile.Infrastructure.Data;

public class UserProfileContext(DbContextOptions<UserProfileContext> options) : DbContext(options)
{
    public DbSet<Domain.UserProfiles.UserProfile> UserProfiles => Set<Domain.UserProfiles.UserProfile>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<UserFollow> UserFollowers => Set<UserFollow>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
