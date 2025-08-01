using AutoMapper;
using MindPulse.Core.Application.DTOs.AnswerOption;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities;
using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Shared.Services
{
    public class AnswerOptionService : IAnswerOptionService
    {
        private readonly IGenericRepository<AnswerOption> _answerRepository;
        private readonly IMapper _mapper;

        public AnswerOptionService(IGenericRepository<AnswerOption> answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<AnswerOptionResponseDTO>>> GetAllAsync()
        {
            var options = await _answerRepository.GetAllAsync();
            var dtos = _mapper.Map<List<AnswerOptionResponseDTO>>(options);
            return new ApiResponse<List<AnswerOptionResponseDTO>>(200, dtos);
        }

        public async Task<ApiResponse<AnswerOptionResponseDTO>> GetByIdAsync(int id)
        {
            var option = await _answerRepository.GetByIdAsync(id);
            if (option == null)
                return new ApiResponse<AnswerOptionResponseDTO>(404, "Opción de respuesta no encontrada.");

            var dto = _mapper.Map<AnswerOptionResponseDTO>(option);
            return new ApiResponse<AnswerOptionResponseDTO>(200, dto);
        }

        public async Task<ApiResponse<AnswerOptionResponseDTO>> CreateAsync(AnswerOptionCreateDTO dto)
        {
            var entity = _mapper.Map<AnswerOption>(dto);
            var created = await _answerRepository.AddAsync(entity);
            var response = _mapper.Map<AnswerOptionResponseDTO>(created);
            return new ApiResponse<AnswerOptionResponseDTO>(201, response);
        }

        public async Task<ApiResponse<AnswerOptionResponseDTO>> UpdateAsync(int id, AnswerOptionUpdateDTO dto)
        {
            var existing = await _answerRepository.GetByIdAsync(id);
            if (existing == null)
                return new ApiResponse<AnswerOptionResponseDTO>(404, "Opción de respuesta no encontrada.");

            _mapper.Map(dto, existing);
            var updated = await _answerRepository.UpdateAsync(existing);
            var response = _mapper.Map<AnswerOptionResponseDTO>(updated);
            return new ApiResponse<AnswerOptionResponseDTO>(200, response);
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var deleted = await _answerRepository.DeleteAsync(id);
            return new ApiResponse<bool>(200, deleted);
        }
    }
}
