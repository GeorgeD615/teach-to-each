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
        }
    }
}
