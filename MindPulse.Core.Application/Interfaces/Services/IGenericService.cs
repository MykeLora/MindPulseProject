using MindPulse.Core.Application.Wrappers;
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
        Task<ApiResponse<Response>> CreateAsync(CreateDTO createDto);
        Task<ApiResponse<Response>> UpdateAsync(UpdateDTO updateDto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
        Task<ApiResponse<Response?>> GetByIdAsync(int id);
        Task<ApiResponse<List<Response>>> GetAllAsync();

    }
}