using AutoMapper;
using MindPulse.Core.Application.DTOs.AnswerOption;
using MindPulse.Core.Application.DTOs.Question;
using MindPulse.Core.Application.DTOs.Questionaries;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Services
{
    public class QuestionnaireService : GenericService<QuestionnaireCreateDTO, QuestionnaireUpdateDTO, Questionnaire, QuestionnaireResponseDTO>, IQuestionnaireService
    {


        private readonly IQuestionaireRepository _questionnaire;
        private readonly IMapper _mapper;
        public QuestionnaireService(IQuestionaireRepository questionaireRepository, IMapper mapper) : base(questionaireRepository, mapper)
        {
            _questionnaire = questionaireRepository;
            _mapper = mapper;

        }

        public async Task<ApiResponse<QuestionnaireResponseDTO>> CreateAsync(QuestionnaireCreateDTO createDto)
        {
            try
            {
                var entity = _mapper.Map<Questionnaire>(createDto);
                var created = await _questionnaire.AddAsync(entity);

                if (created == null)
                    return new ApiResponse<QuestionnaireResponseDTO>(400, "Failed to create questionnaire.");

                var dto = _mapper.Map<QuestionnaireResponseDTO>(created);
                return new ApiResponse<QuestionnaireResponseDTO>(201, dto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<QuestionnaireResponseDTO>(500, $"An error occurred while creating the questionnaire: {ex.Message}");
            }
        }
        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var success = _questionnaire.DeleteAsync(id);
                if (success == null)
                    return new ApiResponse<bool>(404, "Questionnaire not found.");

                return new ApiResponse<bool>(200, true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(500, $"An error occurred while deleting the questionnaire: {ex.Message}");
            }
        }
        public async Task<ApiResponse<bool>> ExistsAsync(int id)
        {
            var entity = await _questionnaire.GetByIdAsync(id);
            return new ApiResponse<bool>(200, entity != null);
        }
        public async Task<ApiResponse<List<QuestionnaireResponseDTO>>> GetAllAsync()
        {
            try
            {
                var list = await _questionnaire.GetAllWithQuestionsAsync();

                var dtoList = list.Select(q => new QuestionnaireResponseDTO
                {
                    Id = q.Id,
                    Title = q.Title,
                    Description = q.Description,
                    Questions = q.Questions?.Select(quest => new QuestionResponseDTO
                    {
                        Id = quest.Id,
                        Text = quest.Text,
                        Type = quest.Type,
                        QuestionnaireId = quest.QuestionnaireId
                    }).ToList()
                }).ToList();

                return new ApiResponse<List<QuestionnaireResponseDTO>>(200, dtoList);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<QuestionnaireResponseDTO>>(500, $"Error retrieving questionnaires with questions: {ex.Message}");
            }
        }
        public async Task<ApiResponse<List<QuestionnaireResponseDTO>>> GetAllWithQuestionsAndOptions()
        {
            try
            {
                var list = await _questionnaire.GetAllWithQuestionsAsync();

                var dtoList = list.Select(q => new QuestionnaireResponseDTO
                {
                    Id = q.Id,
                    Title = q.Title,
                    Description = q.Description,
                    Questions = q.Questions?.Select(quest => new QuestionResponseDTO
                    {
                        Id = quest.Id,
                        Text = quest.Text,
                        Type = quest.Type,
                        QuestionnaireId = quest.QuestionnaireId,
                        AnswerOptions = quest.AnswerOptions?.Select(opt => new AnswerOptionDTO
                        {
                            Id = opt.Id,
                            Text = opt.Text
                        }).ToList()
                    }).ToList()
                }).ToList();

                return new ApiResponse<List<QuestionnaireResponseDTO>>(200, dtoList);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<QuestionnaireResponseDTO>>(500, $"Error retrieving questionnaires with questions: {ex.Message}");
            }
        }
        public async Task<ApiResponse<QuestionnaireResponseDTO?>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _questionnaire.GetByIdAsync(id);
                if (entity == null)
                    return new ApiResponse<QuestionnaireResponseDTO?>(404, "Questionnaire not found.");

                var dto = _mapper.Map<QuestionnaireResponseDTO>(entity);
                return new ApiResponse<QuestionnaireResponseDTO?>(200, dto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<QuestionnaireResponseDTO?>(500, $"Error retrieving questionnaire: {ex.Message}");
            }
        }
        public async Task<ApiResponse<QuestionnaireResponseDTO?>> GetByTitleAsync(string title)
        {
            try
            {
                var all = await _questionnaire.GetAllAsync();
                var match = all.FirstOrDefault(x => x.Title != null && x.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

                if (match == null)
                    return new ApiResponse<QuestionnaireResponseDTO?>(404, "No questionnaire found with that title.");

                var dto = _mapper.Map<QuestionnaireResponseDTO>(match);
                return new ApiResponse<QuestionnaireResponseDTO?>(200, dto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<QuestionnaireResponseDTO?>(500, $"Error searching by title: {ex.Message}");
            }
        }
        public async Task<ApiResponse<QuestionnaireResponseDTO>> UpdateAsync(QuestionnaireUpdateDTO updateDto)
        {
            try
            {
                var entity = _mapper.Map<Questionnaire>(updateDto);
                var updated = await _questionnaire.UpdateAsync(entity);

                if (updated == null)
                    return new ApiResponse<QuestionnaireResponseDTO>(404, "Questionnaire not found.");

                var dto = _mapper.Map<QuestionnaireResponseDTO>(updated);
                return new ApiResponse<QuestionnaireResponseDTO>(200, dto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<QuestionnaireResponseDTO>(500, $"Error updating questionnaire: {ex.Message}");
            }
        }
        public async Task<ApiResponse<List<QuestionnaireSimpleDTO>>> GetAllSimpleAsync()
        {
            try
            {
                var entities = await _questionnaire.GetAllAsync();
                var dtoList = _mapper.Map<List<QuestionnaireSimpleDTO>>(entities);
                return new ApiResponse<List<QuestionnaireSimpleDTO>>(200, dtoList);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<QuestionnaireSimpleDTO>>(500, $"Error retrieving basic questionnaires: {ex.Message}");
            }
        }
    }
}