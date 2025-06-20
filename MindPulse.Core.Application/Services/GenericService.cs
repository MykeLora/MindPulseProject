using AutoMapper;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Task<Response> CreateAsync(CreateDTO createDto)
        {

            var createEntity = _mapper.Map<Entity>(createDto);

            try
            {

                var createdEntity = _repo.AddAsync(createEntity);
                return Task.FromResult(_mapper.Map<Response>(createdEntity));
            }
            catch (Exception)
            {

                throw new Exception("An error occurred while creating the entity.");
            }

        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<List<Response>> GetAllAsync()
        {
            return _mapper.Map<List<Response>>(await _repo.GetAllAsync());
        }

        public Task<Response?> GetByIdAsync(int id)
        {
            return _mapper.Map<Task<Response?>>(_repo.GetByIdAsync(id));
        }

        public async Task<Response> UpdateAsync(UpdateDTO updateDto)
        {

            var entityToUpdate = _mapper.Map<Entity>(updateDto);
            var result =   _repo.UpdateAsync(entityToUpdate);

            return _mapper.Map<Response>(result);
        }

    }
}