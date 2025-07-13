using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindPulse.Core.Domain.Entities;

namespace MindPulse.Infrastructure.Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Name)
                   .HasMaxLength(100)
                   .IsRequired();


            builder.Property(u => u.Name)
                   .HasMaxLength(100);
                   

            builder.Property(u => u.Email)
                   .HasMaxLength(150)
                   .IsRequired();


            builder.Property(u => u.Role)
                   .HasConversion<string>() 
                   .HasMaxLength(20)        
                   .IsRequired();

            builder.Property(u => u.PasswordHash)
                   .IsRequired();


            builder.Property(u => u.IsConfirmed);
            builder.Property(u => u.IsSuspended);
            builder.Property(u => u.FailedLoginAttempts);
            builder.Property(u => u.VerificationToken)
                   .HasMaxLength(200);

            builder.HasMany(u => u.Recommendations)
                   .WithOne(r => r.User)
                   .HasForeignKey(r => r.UserId);

            builder.HasMany(u => u.TestResults)
                   .WithOne(tr => tr.User)
                   .HasForeignKey(tr => tr.UserId);

            builder.HasMany(u => u.EmotionalAnalyses)
                   .WithOne(ea => ea.User)
                   .HasForeignKey(ea => ea.UserId);

            builder.HasMany(u => u.EmotionalRecords)
                   .WithOne(er => er.User)
                   .HasForeignKey(er => er.UserId);
        }
    }

}
