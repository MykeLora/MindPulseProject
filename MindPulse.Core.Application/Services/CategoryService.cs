using MindPulse.Core.Application.DTOs.Categories;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Categories;
using AutoMapper;
using System.Threading.Tasks;
using System.Linq;

namespace MindPulse.Core.Application.Services
{
    public class CategoryService : GenericService<CategoryDTO, CategoryDTO, Category, CategoryDTO>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _categoryRepository = repo;
        }

        // Método para chequear si existe una categoría con ese nombre y devolver ID si existe
        public async Task<(bool exists, int existingId)> CheckDuplicateByNameAsync(string name)
        {
            var existingCategory = await _categoryRepository.GetByNameAsync(name);

            if (existingCategory != null)
                return (true, existingCategory.Id);

            return (false, 0);
        }


        public override async Task<ApiResponse<CategoryDTO>> CreateAsync(CategoryDTO dto)
        {
            var (exists, existingId) = await CheckDuplicateByNameAsync(dto.Name);

            if (exists)
            {
                return new ApiResponse<CategoryDTO>(400,
                    new[] { $"Category with name '{dto.Name}' already exists with ID {existingId}." }.ToList());
            }

            // Si no existe, llamamos al método base para crear
            return await base.CreateAsync(dto);
        }

        public override async Task<ApiResponse<CategoryDTO>> UpdateAsync(CategoryDTO dto)
        {
            var (exists, existingId) = await CheckDuplicateByNameAsync(dto.Name);

            if (exists && existingId != dto.Id)
            {
                return new ApiResponse<CategoryDTO>(400,
                    new[] { $"Category with name '{dto.Name}' already exists with ID {existingId}." }.ToList());
            }

            // Si no hay duplicados o el duplicado es el mismo registro, permitimos actualizar
            return await base.UpdateAsync(dto);
        }
    }
}