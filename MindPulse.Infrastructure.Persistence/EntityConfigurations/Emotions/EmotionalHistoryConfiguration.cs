using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindPulse.Core.Domain.Entities.Emotions;

namespace MindPulse.Infrastructure.Persistence.EntityConfigurations.Emotions
{
    public class EmotionalHistoryConfiguration : IEntityTypeConfiguration<EmotionalHistory>
    {
        public void Configure(EntityTypeBuilder<EmotionalHistory> builder)
        {
            builder.ToTable("EmotionalHistories");

            builder.HasKey(eh => eh.Id);

            builder.Property(eh => eh.Emotion)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(eh => eh.Score)
                   .IsRequired();

            builder.Property(eh => eh.Date)
                   .IsRequired();

            builder.HasOne(eh => eh.User)
                   .WithMany(u => u.EmotionalHistories)
                   .HasForeignKey(eh => eh.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }


}
