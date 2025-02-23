using UserProfile.Domain.UserProfiles.ValueObjects;

namespace UserProfile.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => new {c.Id, c.Name});

        builder.Property(c => c.Id).HasConversion(
            profileId => profileId.Value,
            dbId => new UserProfileId(dbId));

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();
    }
}
