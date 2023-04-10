using DataAccessLayer.Configurations.Base;
using Domain.Entities;
using Domain.Entities.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public new void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.UserName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.FirstName)
                .HasMaxLength(255);

            builder.Property(x => x.LastName)
                .HasMaxLength(255);

            builder.Property(x => x.Password)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
