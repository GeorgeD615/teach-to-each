using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.Domain.Entity;
using TeachToEach.Domain.Helpers;

namespace TeachToEach.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            
            builder.Property(u => u.id).ValueGeneratedOnAdd().HasColumnName("user_id");
            builder.Property(u => u.first_name).HasColumnType("character varying(32)").
                                                HasMaxLength(32).
                                                IsRequired().
                                                HasColumnName("first_name");
            builder.Property(u => u.last_name).HasColumnType("character varying(32)").
                                                HasMaxLength(32).
                                                IsRequired().
                                                HasColumnName("last_name");
            builder.Property(u => u.age).HasColumnType("smallint").
                                                IsRequired().
                                                HasColumnName("age");
            builder.Property(u => u.email).HasColumnType("text").
                                                IsRequired().
                                                HasColumnName("email");
            builder.Property(u => u.password).HasColumnName("password").
                                                HasColumnType("text").
                                                IsRequired();
            builder.Property(u => u.login).HasColumnName("login").
                                                HasColumnType("character varying(32)");

            builder.HasOne(u => u.role).WithMany(r => r.users).HasForeignKey(u => u.role_id);

            builder.HasCheckConstraint("age", "age > 7 AND age < 121");

            builder.HasData(new User()
            {
                id = 1,
                first_name = "Георгий",
                last_name = "Давлятшин",
                email = "g.davlyatshin@gmail.com",
                age = 20,
                role_id = 3,
                password = HashPasswordHelper.HashPassowrd("123456"),
                login = "davlik2003"
            });
        }
    }
}
