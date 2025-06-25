using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string email, List<string> list);
    }
}
