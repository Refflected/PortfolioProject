using Domain.Entities;
using Domain.Entities.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configurations.Base
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Id)
                .IsRequired();

            builder.Property(b => b.CreatedDate)
                .HasAnnotation("DefaultValueSql", "getutcdate()");

            builder.Property(b => b.UpdatedDate)
                .HasAnnotation("DefaultValueSql", "getutcdate()");
        }
    }
}
