﻿using MindPulse.Core.Application.DTOs.User;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {

        Task<UserStatisticsDTO?> GetUserStatisticsAsync(int userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task ChangePasswordAsync( User user);
    }
}
