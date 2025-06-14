using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {

        Task LoginAsync(User user);
        Task<bool> RegisterAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> GetByTokenAsync(string token);
        Task ChangePasswordAsync( User user);

    }
}
