using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.Repositories
{
    public class UserResponseRepository : IUserResponseRepository
    {
        private readonly ApplicationContext _context;
        public UserResponseRepository(ApplicationContext context)
        {
            _context = context;
        }
        
        public async Task<UserResponse> AddAsync(UserResponse entity)
        {
            await _context.UserResponses.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<UserResponse>> GetByUserIdAsync(int userId)
        {
            return await _context.UserResponses
                .Where(ur => ur.UserId == userId)
                .ToListAsync();
        }
    }
}
