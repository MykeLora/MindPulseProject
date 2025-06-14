using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindPulse.Core.Domain.Entities.Emotions;

namespace MindPulse.Infrastructure.Persistence.EntityConfigurations
{
    public class EmotionRecordConfiguration : IEntityTypeConfiguration<EmotionRecord>
    {
        public void Configure(EntityTypeBuilder<EmotionRecord> builder)
        {
            builder.ToTable("EmotionalRecords");

            builder.HasKey(er => er.Id);

            builder.Property(er => er.Text)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(er => er.DetectedEmotion)
                   .HasMaxLength(100);

            builder.Property(er => er.Confidence)
                   .HasColumnType("float");

            builder.HasOne(er => er.User)
                   .WithMany(u => u.EmotionalRecords)
                   .HasForeignKey(er => er.UserId);
        }
    }

}
