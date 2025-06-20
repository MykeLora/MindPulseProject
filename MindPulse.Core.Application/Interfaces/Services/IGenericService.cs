using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IGenericService<CreateDTO, UpdateDTO, Entity, Response>
    where CreateDTO : class
    where UpdateDTO : class
    where Entity : class
    where Response : class
    {
        Task<Response> CreateAsync(CreateDTO createDto);
        Task<Response> UpdateAsync(UpdateDTO updateDto);
        Task DeleteAsync(int id);
        Task<Response?> GetByIdAsync(int id);
        Task<List<Response>> GetAllAsync();

    }
}