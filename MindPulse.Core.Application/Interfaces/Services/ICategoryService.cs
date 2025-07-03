using MindPulse.Core.Application.DTOs.Categories;
using MindPulse.Core.Domain.Entities.Categories;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface ICategoryService : IGenericService<CategoryDTO, CategoryDTO, Category, CategoryDTO>
    {
    }
}
