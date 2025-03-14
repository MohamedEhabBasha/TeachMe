namespace UserProfile.Infrastructure.Data.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<Domain.UserProfiles.UserProfile>
{
    public void Configure(EntityTypeBuilder<Domain.UserProfiles.UserProfile> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasConversion(
            profileId => profileId.Value,
            dbId => new UserProfileId(dbId));

        builder.Property(u => u.Name).HasMaxLength(50).IsRequired();

        builder.Property(u => u.Role).HasMaxLength(50).IsRequired();

        builder.Property(u => u.Introduction).HasMaxLength(255);

        builder.Property(u => u.Description).HasMaxLength(600);

        builder.HasMany(u => u.Categories)
            .WithOne()
            .HasForeignKey(c => c.Id);

        builder.HasMany(u => u.UserFollowers)
            .WithOne()
            .HasForeignKey(f => f.Id);

        builder.ComplexProperty(
            u => u.Photo, photoBuilder =>
            {
                photoBuilder.Property(p => p.Url)
                    .HasMaxLength(500);
                photoBuilder.Property(p => p.PublicId)
                    .HasMaxLength(500);
            });
    }
}
