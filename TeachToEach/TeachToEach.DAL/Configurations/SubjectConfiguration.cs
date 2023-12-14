using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.Domain.Entity;
using TeachToEach.Domain.Enum;

namespace TeachToEach.DAL.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {            
            builder.Property(u => u.id).ValueGeneratedOnAdd().
                                        HasColumnName("subject_id");
            builder.Property(u => u.name).HasColumnType("character varying(32)").
                                        HasMaxLength(32).
                                        IsRequired().
                                        HasColumnName("name");
            builder.HasData(new Subject()
            {
                id = 1,
                name = "Математика",
            }, new Subject()
            {
                id = 2,
                name = "Литература",
            }, new Subject()
            {
                id = 3,
                name = "Биология",
            }, new Subject()
            {
                id = 4,
                name = "История",
            }, new Subject()
            {
                id = 5,
                name = "Обществознание",
            }, new Subject()
            {
                id = 6,
                name = "Английский язык",
            }, new Subject()
            {
                id = 7,
                name = "Информатика",
            }, new Subject()
            {
                id = 8,
                name = "Музыка",
            }, new Subject()
            {
                id = 9,
                name = "Физика",
            },
            new Subject()
            {
                id = 10,
                name = "Рисование",
            });
        }
    }
}
