using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindPulse.Core.Domain.Entities.Evaluations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MindPulse.Infrastructure.Persistence.EntityConfigurations.Evaluations
{
    public class AiResponseConfiguration : IEntityTypeConfiguration<AiResponse>
    {
        public void Configure(EntityTypeBuilder<AiResponse> builder)
        {
            builder.ToTable("AiResponses");

            builder.HasKey(ar => ar.Id);

            builder.Property(ar => ar.ChatResponse)
                .IsRequired()
                .HasMaxLength(2000); 

            builder.HasOne(ar => ar.User)
                .WithMany()
                .HasForeignKey(ar => ar.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
