using MindPulse.Core.Application.DTOs.Categories;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Domain.Entities.Categories;
using AutoMapper;

namespace MindPulse.Core.Application.Services
{
    public class CategoryService : GenericService<CategoryDTO, CategoryDTO, Category, CategoryDTO>, ICategoryService
    {
        public CategoryService(ICategoryRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}
