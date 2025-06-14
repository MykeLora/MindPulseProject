using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Application.DTOs.User;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities;
using MindPulse.Infrastructure.Persistence.Context;
using MindPulse.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        private readonly ApplicationContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ApplicationContext context, ILogger<UserRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task ChangePasswordAsync(User user)
        {
            try
            {
                var existingUser = await _context.Users.FindAsync(user.Id);
                if (existingUser == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found.", user.Id);
                    return; 
                }

                
                var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

                existingUser.PasswordHash = newHashedPassword;
                existingUser.LastModified = DateTime.UtcNow;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password for user {UserId}", user.Id);
                throw;
            }

        }
        public async Task<User?> GetByTokenAsync(string token)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.VerificationToken == token);

                if (user == null)
                {
                    _logger.LogWarning("User not found with the provided token.");
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user by token: {Token}", token);
                throw;
            }
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            try
            {

                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
       
            }
            catch (Exception)
            {
                _logger.LogError("Error retrieving user by email: {Email}", email);
                throw;
            }
        }
        public async Task<UserStatisticsDTO?> GetUserStatisticsAsync(int userId)
        {
            var userStats = await _context.Users
               .Where(u => u.Id == userId)
               .Select(u => new UserStatisticsDTO
               {
                   UserId = u.Id,
                   UserName = u.UserName ?? u.Name,
                   TotalTests = u.Tests.Count,
                   TotalRecommendations = u.Recommendations.Count,
                   TotalEmotionalAnalyses = u.EmotionalAnalyses.Count,
                   TotalEmotionalRecords = u.EmotionalRecords.Count
               })
               .FirstOrDefaultAsync();

            return userStats;

        }
    }
}
