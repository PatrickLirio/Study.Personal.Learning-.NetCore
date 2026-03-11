using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAuthApiProperArchitecture.Domain.Entities;


namespace UserAuthApiProperArchitecture.Infrastructure.Configurations
{
    internal class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Table name
            builder.ToTable("Users");

            //Primary key
            builder.HasKey(u => u.Id);

            // Email must be unique and not null, max255 chars
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);
             builder.HasIndex(u => u.Email)
                .IsUnique();


            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(u => u.Username)
                .IsUnique();
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            // PasswordHash is required
            builder.Property(u => u.PasswordHash)
                .IsRequired();
        }
    }

}
