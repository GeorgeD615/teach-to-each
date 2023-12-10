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
    public class TeacherStudentConfiguration : IEntityTypeConfiguration<TeacherStudent>
    {
        public void Configure(EntityTypeBuilder<TeacherStudent> builder)
        {
            builder.Property(r => r.id).ValueGeneratedOnAdd().HasColumnName("relation_id");

            builder.HasOne(r => r.teacher).WithMany(t => t.relation_as_teacher).HasForeignKey(r => r.teacher_id);
            builder.HasOne(r => r.student).WithMany(s => s.relation_as_student).HasForeignKey(r => r.student_id);
            builder.HasOne(r => r.subject).WithMany(sub => sub.teacher_student_relations).HasForeignKey(r => r.subject_id);
            builder.HasOne(r => r.status).WithMany(st => st.relations).HasForeignKey(r => r.status_id);
            builder.HasOne(r => r.rating).WithOne(r => r.relation).HasForeignKey<Rating>(r => r.relation_id);

            builder.HasCheckConstraint("teacher_id", "teacher_id <> student_id");
        }
    }
}
