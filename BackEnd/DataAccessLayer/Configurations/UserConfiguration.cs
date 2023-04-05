using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.UserName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.FirstName)
                .HasMaxLength(30);

            builder.Property(x => x.LastName)
                .HasMaxLength(30);

            builder.Property(x => x.Email)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
