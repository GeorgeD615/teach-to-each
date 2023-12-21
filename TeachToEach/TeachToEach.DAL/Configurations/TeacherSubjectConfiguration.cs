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
    public class TeacherSubjectConfiguration : IEntityTypeConfiguration<TeacherSubject>
    {
        public void Configure(EntityTypeBuilder<TeacherSubject> builder)
        {
            builder.Property(r => r.id).ValueGeneratedOnAdd().HasColumnName("relation_id");

            builder.HasOne(r => r.teacher).WithMany(t => t.subjects_for_teacher).HasForeignKey(r => r.teacher_id);
            builder.HasOne(r => r.subject).WithMany(s => s.teacher_subject_relation).HasForeignKey(r => r.subject_id);

            builder.HasData(new TeacherSubject()
            {
                id = 1,
                teacher_id = 1,
                subject_id = 1
            },
            new TeacherSubject()
            {
                id = 2,
                teacher_id = 1,
                subject_id = 2
            },
            new TeacherSubject()
            {
                id = 3,
                teacher_id = 2,
                subject_id = 1
            },
            new TeacherSubject()
            {
                id = 4,
                teacher_id = 2,
                subject_id = 4
            },
            new TeacherSubject()
            {
                id = 5,
                teacher_id = 2,
                subject_id = 5
            },
            new TeacherSubject()
            {
                id = 6,
                teacher_id = 3,
                subject_id = 1
            },
            new TeacherSubject()
            {
                id = 7,
                teacher_id = 3,
                subject_id = 10
            },
            new TeacherSubject()
            {
                id = 8,
                teacher_id = 4,
                subject_id = 6
            },
            new TeacherSubject()
            {
                id = 9,
                teacher_id = 4,
                subject_id = 7
            },
            new TeacherSubject()
            {
                id = 10,
                teacher_id = 5,
                subject_id = 2
            },
            new TeacherSubject()
            {
                id = 11,
                teacher_id = 5,
                subject_id = 6
            },
            new TeacherSubject()
            {
                id = 12,
                teacher_id = 6,
                subject_id = 8
            },
            new TeacherSubject()
            {
                id = 13,
                teacher_id = 6,
                subject_id = 5
            },
            new TeacherSubject()
            {
                id = 14,
                teacher_id = 7,
                subject_id = 6
            },
            new TeacherSubject()
            {
                id = 15,
                teacher_id = 7,
                subject_id = 4
            },
            new TeacherSubject()
            {
                id = 16,
                teacher_id = 8,
                subject_id = 3
            },
            new TeacherSubject()
            {
                id = 17,
                teacher_id = 9,
                subject_id = 8
            },
            new TeacherSubject()
            {
                id = 18,
                teacher_id = 9,
                subject_id = 9
            },
            new TeacherSubject()
            {
                id = 19,
                teacher_id = 7,
                subject_id = 3
            },
            new TeacherSubject()
            {
                id = 20,
                teacher_id = 10,
                subject_id = 7
            });
        }
    }
}
