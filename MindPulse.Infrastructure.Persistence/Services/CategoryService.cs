using MindPulse.Core.Application.DTOs.Categories;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Domain.Entities.Categories;
using AutoMapper;
using MindPulse.Core.Application.Services;

namespace MindPulse.Infrastructure.Persistence.Services
{
    public class CategoryService : GenericService<CategoryDTO, CategoryDTO, Category, CategoryDTO>, ICategoryService
    {
        public CategoryService(ICategoryRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}
