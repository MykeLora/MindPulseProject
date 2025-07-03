using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Domain.Entities.Emotions;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Core.Domain.Entities.Recommendations;
using MindPulse.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindPulse.Core.Domain.Commons;
using MindPulse.Infrastructure.Persistence.EntityConfigurations.Emotions;
using MindPulse.Infrastructure.Persistence.EntityConfigurations.Recommdations;
using MindPulse.Infrastructure.Persistence.EntityConfigurations.Evaluations;
using MindPulse.Infrastructure.Persistence.EntityConfigurations.Categories;
using MindPulse.Core.Domain.Entities.Categories;
using MindPulse.Infrastructure.Persistence.EntityConfigurations;


namespace MindPulse.Infrastructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }


        // DbSet properties for your entities

        #region DbSets
            public DbSet<User> Users { get; set; }
            public DbSet<EmotionalAnalysis> EmotionAnalyses { get; set; }
            public DbSet<Questionnaire> Questionnaires { get; set; }
            public DbSet<Question> Questions { get; set; }
            public DbSet<AnswerOption> AnswerOptions { get; set; }
            public DbSet<TestResult> TestResults { get; set; }
            public DbSet<UserResponse> UserResponses { get; set; }
            public DbSet<Recommendation> Recommendations { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<EducationalContent> EducationalContents { get; set; }
            public DbSet<EmotionRecord> EmotionalRecords { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations from the assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            modelBuilder.ApplyConfiguration(new EmotionalHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new EmotionAnalysisConfiguration());
            modelBuilder.ApplyConfiguration(new EmotionRecordConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionnaireConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerOptionConfiguration());
            modelBuilder.ApplyConfiguration(new TestResultConfiguration());
            modelBuilder.ApplyConfiguration(new UserResponseConfiguration());
            modelBuilder.ApplyConfiguration(new RecommendationConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new EducationalContentConfiguration());

            base.OnModelCreating(modelBuilder);
        }




    }
}
