namespace Accounting.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasConversion(
            userId => userId.Value,
            dbId => new UserId(dbId));

        builder.Property(u => u.Name).HasMaxLength(50).IsRequired();

        builder.Property(u => u.UserName).HasMaxLength(255);

        builder.HasIndex(u => u.UserName).IsUnique();

        builder.ComplexProperty(
            u => u.Role, roleBuilder =>
            {
                roleBuilder.Property(u => u.Name)
                    .HasColumnName(nameof(User.Role))
                    .HasMaxLength(50)
                    .IsRequired();
            });
    }
}
