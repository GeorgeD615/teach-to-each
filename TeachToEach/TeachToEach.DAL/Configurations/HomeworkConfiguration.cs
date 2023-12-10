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
            builder.Property(h => h.deadline).HasColumnName("deadline").IsRequired(false).HasColumnType("text");
            builder.Property(h => h.solution_time).HasColumnName("solution_time");
            builder.Property(h => h.is_completed).HasDefaultValue(false).HasColumnName("is_completed").HasColumnType("bool");
            builder.Property(h => h.solution).HasColumnType("text").HasColumnName("solution").IsRequired(false);
            builder.Property(h => h.teacher_comment).HasColumnName("teacher_comment").IsRequired(false);

            builder.HasOne(h => h.relation).WithMany(r => r.homeworks).HasForeignKey(h => h.relation_id);
        }
    }
}
