using AutoMapper;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Services
{
    public class GenericService<CreateDTO, UpdateDTO, Entity, Response> : IGenericService<CreateDTO, UpdateDTO, Entity, Response>
         where CreateDTO : class
         where UpdateDTO : class
         where Entity : class
         where Response : class
    {
        private readonly IGenericRepository<Entity> _repo;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public virtual async Task<ApiResponse<Response>> CreateAsync(CreateDTO createDto)
        {
            try
            {
                var createEntity = _mapper.Map<Entity>(createDto);
                var createdEntity = await _repo.AddAsync(createEntity); // AWAIT agregado
                var response = _mapper.Map<Response>(createdEntity);

                return new ApiResponse<Response>(200, response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<Response>(500, $"An error occurred while creating the entity: {ex.Message}");
            }
        }

        public virtual async Task<ApiResponse<Response>> UpdateAsync(UpdateDTO updateDto)
        {
            try
            {
                var entityToUpdate = _mapper.Map<Entity>(updateDto);
                var result = await _repo.UpdateAsync(entityToUpdate); // AWAIT agregado

                if (result == null)
                    return new ApiResponse<Response>(404, "Entity not found.");

                var response = _mapper.Map<Response>(result);
                return new ApiResponse<Response>(200, response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<Response>(500, $"An error occurred while updating the entity: {ex.Message}");
            }
        }

        public virtual async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var result = await _repo.DeleteAsync(id); // AWAIT agregado
                if (!result)
                    return new ApiResponse<bool>(404, "Entity not found.");

                return new ApiResponse<bool>(200, true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(500, $"An error occurred while deleting the entity: {ex.Message}");
            }
        }

        public virtual async Task<ApiResponse<Response?>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repo.GetByIdAsync(id);
                if (entity == null)
                    return new ApiResponse<Response?>(404, "Entity not found.");

                var response = _mapper.Map<Response>(entity);
                return new ApiResponse<Response?>(200, response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<Response?>(500, $"An error occurred while retrieving the entity: {ex.Message}");
            }
        }

        public virtual async Task<ApiResponse<List<Response>>> GetAllAsync()
        {
            try
            {
                var entities = await _repo.GetAllAsync();
                var responseList = _mapper.Map<List<Response>>(entities);
                return new ApiResponse<List<Response>>(200, responseList);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<Response>>(500, $"An error occurred while retrieving all entities: {ex.Message}");
            }
        }
    }
}