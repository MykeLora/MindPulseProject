using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Domain.Entities.Recommendations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.EntityConfigurations.Recommdations
{
    public class EducationalContentConfiguration : IEntityTypeConfiguration<EducationalContent>
    {
        public void Configure(EntityTypeBuilder<EducationalContent> builder)
        {
            builder.ToTable("EducationalContents");

            builder.HasKey(ec => ec.Id);

            builder.Property(ec => ec.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(ec => ec.Type)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(ec => ec.Description)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(ec => ec.Url)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.HasOne(ec => ec.Category)
                   .WithMany(c => c.EducationalContents)
                   .HasForeignKey(ec => ec.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
