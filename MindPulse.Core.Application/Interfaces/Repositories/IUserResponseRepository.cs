using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Repositories
{
    public interface IUserResponseRepository
    {
        Task<UserResponse> AddAsync(UserResponse response);
        Task<List<UserResponse>> GetByUserIdAsync(int userId);

    }
}
