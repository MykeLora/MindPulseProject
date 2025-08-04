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
    public class TestResultConfiguration : IEntityTypeConfiguration<TestResult>
    {
        public void Configure(EntityTypeBuilder<TestResult> builder)
        {
            builder.ToTable("TestResults");

            builder.HasKey(tr => tr.Id);

            builder.Property(tr => tr.Confidence)
                   .IsRequired();

            builder.Property(tr => tr.Summary)
                   .HasMaxLength(1000);

            builder.HasOne(tr => tr.User)
                   .WithMany(u => u.TestResults)
                   .HasForeignKey(tr => tr.UserId);

            builder.HasOne(tr => tr.Questionnaire)
               .WithMany(q => q.TestResults)
               .HasForeignKey(tr => tr.QuestionnaireId);

        }
    }
}