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
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.Property(h => h.id).ValueGeneratedOnAdd().HasColumnName("homework_id");
            builder.Property(h => h.description).HasColumnType("description").HasColumnType("text").HasMaxLength(1000).IsRequired();
            builder.Property(h => h.deadline).HasColumnName("deadline").IsRequired(false).HasColumnType("timestamp without time zone");
            builder.Property(h => h.solution_time).HasColumnName("solution_time").IsRequired(false).HasColumnType("timestamp without time zone");
            builder.Property(h => h.is_completed).HasDefaultValue(false).HasColumnName("is_completed").HasColumnType("bool");
            builder.Property(h => h.solution).HasColumnType("text").HasColumnName("solution").IsRequired(false);
            builder.Property(h => h.teacher_comment).HasColumnName("teacher_comment").IsRequired(false);

            builder.HasOne(h => h.relation).WithMany(r => r.homeworks).HasForeignKey(h => h.relation_id);

            builder.HasData(new Homework()
            {
                id = 1,
                relation_id = 1,
                deadline = new DateTime(2024, 2, 24, 12, 0, 0),
                description = "Прочитать Доктор Живаго"
            },
            new Homework()
            {
                id = 2,
                relation_id = 1,
                deadline = new DateTime(2024, 2, 24, 12, 0, 0),
                description = "Прочитать Анну Каренину"
            },
            new Homework()
            {
                id = 3,
                relation_id = 2,
                deadline = new DateTime(2024, 2, 24, 12, 0, 0),
                description = "Прочитать Доктор Живаго"
            },
            new Homework()
            {
                id = 4,
                relation_id = 2,
                deadline = new DateTime(2024, 2, 24, 12, 0, 0),
                description = "Прочитать Анну Каренину"
            },
            new Homework()
            {
                id = 5,
                relation_id = 6,
                deadline = new DateTime(2024, 3, 15, 12, 0, 0),
                description = "Параграф 13(Реформа Столыпина)"
            },
            new Homework()
            {
                id = 6,
                relation_id = 7,
                deadline = new DateTime(2024, 3, 15, 12, 0, 0),
                description = "Параграф 13(Реформа Столыпина)"
            });
        }
    }
}
