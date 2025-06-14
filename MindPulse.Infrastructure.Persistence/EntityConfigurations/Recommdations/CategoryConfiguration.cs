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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Description)
                   .IsRequired()
                   .HasMaxLength(300);

            
            builder.HasMany(c => c.EducationalContents)
                   .WithOne(ec => ec.Category)  
                   .HasForeignKey(ec => ec.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

    
            builder.HasMany(c => c.Recommendations)
                   .WithOne(r => r.Category)  
                   .HasForeignKey(r => r.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
