using UserProfile.Domain.UserProfiles.ValueObjects;

namespace UserProfile.Infrastructure.Data.Configurations;

public class UserFollowConfiguration : IEntityTypeConfiguration<UserFollow>
{
    public void Configure(EntityTypeBuilder<UserFollow> builder)
    {
        builder.HasKey(f => new {f.Id, f.StudentId});

        builder.Property(f => f.Id).HasConversion(
            profileId => profileId.Value,
            dbId => new UserProfileId(dbId));

        builder.Property(u => u.StudentId).HasConversion(
            profileId => profileId.Value,
            dbId => new UserProfileId(dbId));
    }
}
