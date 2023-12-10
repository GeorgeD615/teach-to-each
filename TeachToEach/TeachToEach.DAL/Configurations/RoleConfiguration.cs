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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(u => u.id).ValueGeneratedOnAdd().
                                        HasColumnName("role_id");
            builder.Property(u => u.name).HasColumnType("character varying(32)").
                                        HasMaxLength(32).
                                        IsRequired().
                                        HasColumnName("name");
            builder.HasData(new Role()
            {
                id = 1,
                name = "User"
            }, new Role()
            {
                id = 2,
                name = "Moderator"
            }, new Role()
            {
                id = 3,
                name = "Admin"
            });
        }
    }
}
