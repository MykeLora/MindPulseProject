using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.EntityConfigurations.Evaluations
{
    public class AnswerOptionConfiguration : IEntityTypeConfiguration<AnswerOption>
    {
        public void Configure(EntityTypeBuilder<AnswerOption> builder)
        {
            builder.ToTable("AnswerOptions");

            builder.HasKey(ao => ao.Id);

            builder.Property(ao => ao.Text)
                   .HasMaxLength(300)
                   .IsRequired();

            builder.Property(ao => ao.Value)
                   .IsRequired();

            builder.HasOne(ao => ao.Question)
                   .WithMany(q => q.AnswerOptions)
                   .HasForeignKey(ao => ao.QuestionId);

        }
    }


}
