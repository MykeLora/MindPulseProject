using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IPassWordHelperService
    {
        string HashPassword(string password);
        string GenerateTemporalPassword(int longitud = 10);
    }
}
