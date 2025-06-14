using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Domain.Entities.Emotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.EntityConfigurations
{
    public class EmotionAnalysisConfiguration : IEntityTypeConfiguration<EmotionalAnalysis>
    {
        public void Configure(EntityTypeBuilder<EmotionalAnalysis> builder)
        {
            builder.ToTable("EmotionAnalysis");

            builder.HasKey(ea => ea.Id);

            builder.Property(ea => ea.InputText)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(ea => ea.AnalysisDate)
                .IsRequired();
        }
    }

}