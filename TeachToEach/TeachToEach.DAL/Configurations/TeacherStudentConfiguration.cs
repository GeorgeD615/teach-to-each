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

            builder.HasData(new TeacherStudent()
            {
                id = 1,
                teacher_id = 1,
                student_id = 3,
                subject_id = 2,
                status_id = 2
            },
            new TeacherStudent()
            {
                id = 2,
                teacher_id = 1,
                student_id = 4,
                subject_id = 2,
                status_id = 2
            },
            new TeacherStudent()
            {
                id = 3,
                teacher_id = 1,
                student_id = 8,
                subject_id = 1,
                status_id = 1
            },
            new TeacherStudent()
            {
                id = 4,
                teacher_id = 2,
                student_id = 9,
                subject_id = 5,
                status_id = 2
            },
            new TeacherStudent()
            {
                id = 5,
                teacher_id = 2,
                student_id = 10,
                subject_id = 5,
                status_id = 2
            },
            new TeacherStudent()
            {
                id = 6,
                teacher_id = 2,
                student_id = 8,
                subject_id = 4,
                status_id = 2
            },
            new TeacherStudent()
            {
                id = 7,
                teacher_id = 2,
                student_id = 1,
                subject_id = 4,
                status_id = 1
            },
            new TeacherStudent()
            {
                id = 8,
                teacher_id = 3,
                student_id = 1,
                subject_id = 10,
                status_id = 2
            },
            new TeacherStudent()
            {
                id = 9,
                teacher_id = 3,
                student_id = 5,
                subject_id = 10,
                status_id = 2
            },
            new TeacherStudent()
            {
                id = 10,
                teacher_id = 3,
                student_id = 6,
                subject_id = 10,
                status_id = 1
            },
            new TeacherStudent()
            {
                id = 11,
                teacher_id = 8,
                student_id = 7,
                subject_id = 3,
                status_id = 2
            },
            new TeacherStudent()
            {
                id = 12,
                teacher_id = 8,
                student_id = 6,
                subject_id = 3,
                status_id = 2
            });
        }
    }
}
