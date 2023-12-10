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
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.Property(r => r.id).ValueGeneratedOnAdd().HasColumnName("rating_id");
            builder.Property(r => r.rating_value).HasColumnName("value").
                                                    HasColumnType("smallint").
                                                    IsRequired();

            builder.Property(r => r.review).HasColumnName("review").
                                                    HasColumnType("text").
                                                    HasMaxLength(200);


            builder.HasCheckConstraint("value", "value > 0 AND value < 6");
                                                    
        }
    }
}
