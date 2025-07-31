using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Application.DTOs.User.Admin;
using MindPulse.Core.Domain.Entities;
using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<UserAdminCreateDTO, UserAdminUpdateDTO, User, UserResponseAdminDTO>
    {

    }
}
