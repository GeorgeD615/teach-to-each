using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.Domain.Entity;

namespace TeachToEach.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.id).ValueGeneratedOnAdd();
            builder.Property(u => u.first_name).HasColumnType("character varaing(32)").
                                                HasMaxLength(30).
                                                IsRequired().
                                                HasColumnName("first_name");
            builder.Property(u => u.last_name).HasColumnType("character varying(32)").
                                                HasMaxLength(30).
                                                IsRequired().
                                                HasColumnName("last_name");
            builder.Property(u => u.age).HasColumnType("integer").
                                                IsRequired().
                                                HasColumnName("age");
        }
    }
}
