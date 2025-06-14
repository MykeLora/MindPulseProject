using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindPulse.Core.Domain.Entities.Evaluations;

namespace MindPulse.Infrastructure.Persistence.EntityConfigurations
{
    public class QuestionnaireConfiguration : IEntityTypeConfiguration<Questionnaire>
    {
        public void Configure(EntityTypeBuilder<Questionnaire> builder)
        {
            builder.ToTable("Questionnaires");

            builder.HasKey(q => q.Id);

            builder.Property(q => q.Title)
                   .HasMaxLength(200);

            builder.Property(q => q.Description)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.HasMany(q => q.Questions)
                   .WithOne(qs => qs.Questionnaire)
                   .HasForeignKey(qs => qs.QuestionnaireId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(q => q.TestResults)
                   .WithOne(tr => tr.Questionnaire)
                   .HasForeignKey(tr => tr.QuestionnaireId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
