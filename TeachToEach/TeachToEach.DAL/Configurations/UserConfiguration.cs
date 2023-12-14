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
                                                IsRequired(false).
                                                HasColumnName("email");
            builder.Property(u => u.password).HasColumnName("password").
                                                HasColumnType("text").
                                                IsRequired();
            builder.Property(u => u.login).HasColumnName("login").
                                                HasColumnType("character varying(32)");

            builder.HasOne(u => u.role).WithMany(r => r.users).HasForeignKey(u => u.role_id);

            builder.HasCheckConstraint("age", "age > 7 AND age < 121");
            builder.HasIndex(u => u.login).IsUnique();

            builder.HasData(new User()
            {
                id = 1,
                first_name = "Георгий",
                last_name = "Давлятшин",
                email = "g.davlyatshin@gmail.com",
                age = 20,
                role_id = 3,
                password = HashPasswordHelper.HashPassowrd("G123456D"),
                login = "davlik2003"
            },
            new User()
            {
                id = 2,
                first_name = "Полина",
                last_name = "Антропова",
                email = "p.antropova@gmail.com",
                age = 20,
                role_id = 3,
                password = HashPasswordHelper.HashPassowrd("P123456A"),
                login = "poliantr"
            },
            new User()
            {
                id = 3,
                first_name = "Настасья",
                last_name = "Смирнягина",
                email = "nassmir@gmail.com",
                age = 20,
                role_id = 1,
                password = HashPasswordHelper.HashPassowrd("N123456S"),
                login = "nassmir"
            },
            new User()
            {
                id = 4,
                first_name = "Егор",
                last_name = "Воронцов",
                age = 20,
                role_id = 1,
                password = HashPasswordHelper.HashPassowrd("E123456V"),
                login = "c0nda"
            },
            new User()
            {
                id = 5,
                first_name = "Анна",
                last_name = "Бакирова",
                email = "a.bakirova@gmail.com",
                age = 25,
                role_id = 1,
                password = HashPasswordHelper.HashPassowrd("A123456B"),
                login = "bakirova"
            },
            new User()
            {
                id = 6,
                first_name = "Никита",
                last_name = "Варыгин",
                age = 20,
                role_id = 1,
                password = HashPasswordHelper.HashPassowrd("N123456V"),
                login = "varigin"
            },
            new User()
            {
                id = 7,
                first_name = "Михаэль",
                last_name = "Павлов",
                email = "micapic@gmail.com",
                age = 20,
                role_id = 1,
                password = HashPasswordHelper.HashPassowrd("M123456P"),
                login = "micapic"
            },
            new User()
            {
                id = 8,
                first_name = "Мария",
                last_name = "Грибанова",
                age = 20,
                role_id = 1,
                password = HashPasswordHelper.HashPassowrd("M123456G"),
                login = "mgrib"
            },
            new User()
            {
                id = 9,
                first_name = "Екатерина",
                last_name = "Максимова",
                email = "e.maksimova@gmail.com",
                age = 24,
                role_id = 1,
                password = HashPasswordHelper.HashPassowrd("E123456M"),
                login = "ekatmaksim"
            },
            new User()
            {
                id = 10,
                first_name = "Максим",
                last_name = "Коровкин",
                email = "maksikov77@gmail.com",
                age = 40,
                role_id = 1,
                password = HashPasswordHelper.HashPassowrd("M123456K"),
                login = "77max"
            });
        }
    }
}
