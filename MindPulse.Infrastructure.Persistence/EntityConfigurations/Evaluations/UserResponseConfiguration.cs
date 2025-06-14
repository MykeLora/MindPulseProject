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
    public class UserResponseConfiguration : IEntityTypeConfiguration<UserResponse>
    {
        public void Configure(EntityTypeBuilder<UserResponse> builder)
        {
            builder.ToTable("UserResponses");

            builder.HasKey(ur => ur.Id);

       
            builder.HasOne(ur => ur.User)
                   .WithMany(u => u.UserResponses) 
                   .HasForeignKey(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(ur => ur.Question)
                   .WithMany()
                   .HasForeignKey(ur => ur.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasOne(ur => ur.AnswerOption)
                   .WithMany()
                   .HasForeignKey(ur => ur.AnswerOptionId)
                   .OnDelete(DeleteBehavior.SetNull); 

      
            builder.HasOne(ur => ur.TestResult)
                   .WithMany()
                   .HasForeignKey(ur => ur.TestResultId)
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
