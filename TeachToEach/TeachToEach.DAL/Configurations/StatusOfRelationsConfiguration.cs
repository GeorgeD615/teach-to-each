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
    public class StatusOfRelationsConfiguration : IEntityTypeConfiguration<StatusOfRelation>
    {
        public void Configure(EntityTypeBuilder<StatusOfRelation> builder)
        {
            builder.Property(r => r.id).ValueGeneratedOnAdd().HasColumnName("status_id");
            builder.Property(r => r.name).HasColumnType("character varying(32)").HasColumnName("name");

            builder.HasData(new StatusOfRelation()
            {
                id = 1,
                name = "Заявка на рассмотрении"
            }, new StatusOfRelation()
            {
                id = 2,
                name = "Заявка принята"
            }, new StatusOfRelation()
            {
                id = 3,
                name = "Заявка отклонена"
            });
        }
    }
}
