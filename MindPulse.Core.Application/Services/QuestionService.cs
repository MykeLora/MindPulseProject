using AutoMapper;
using MindPulse.Core.Application.DTOs.AnswerOption;
using MindPulse.Core.Application.DTOs.Question;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Services
{
    public class QuestionService : GenericService<QuestionCreateDTO, QuestionUpdateDTO, Question, QuestionResponseDTO>, IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
            : base(questionRepository, mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<QuestionResponseDTO>>> GetAllWithDetailsAsync()
        {
            try
            {
                var questions = await _questionRepository.GetAllWithIncludes(new List<string> { "Questionnaire", "AnswerOptions" });

                var responseList = questions.Select(q => new QuestionResponseDTO
                {
                    Id = q.Id,
                    Text = q.Text,
                    Type = q.Type,
                    QuestionnaireId = q.QuestionnaireId,
                    QuestionnaireTitle = q.Questionnaire?.Title,
                    AnswerOptions = q.AnswerOptions?
                        .Select(opt => new AnswerOptionDTO
                        {
                            Id = opt.Id,
                            Text = opt.Text
                        }).ToList()
                }).ToList();

                return new ApiResponse<List<QuestionResponseDTO>>(200, responseList);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<QuestionResponseDTO>>(500, $"Error loading questions: {ex.Message}");
            }
        }

        public override async Task<ApiResponse<QuestionResponseDTO>> CreateAsync(QuestionCreateDTO createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.Text))
                return new ApiResponse<QuestionResponseDTO>(400, "Question text cannot be empty.");

            try
            {
                var entity = _mapper.Map<Question>(createDto);
                var created = await _questionRepository.AddAsync(entity);

                if (created == null)
                    return new ApiResponse<QuestionResponseDTO>(400, "Failed to create question.");

                var dto = _mapper.Map<QuestionResponseDTO>(created);
                return new ApiResponse<QuestionResponseDTO>(201, dto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<QuestionResponseDTO>(500, $"Error creating question: {ex.Message}");
            }
        }

        public override async Task<ApiResponse<QuestionResponseDTO>> UpdateAsync(QuestionUpdateDTO updateDto)
        {
            if (string.IsNullOrWhiteSpace(updateDto.Text))
                return new ApiResponse<QuestionResponseDTO>(400, "Question text cannot be empty.");

            try
            {
                var existing = await _questionRepository.GetByIdAsync(updateDto.Id);
                if (existing == null)
                    return new ApiResponse<QuestionResponseDTO>(404, "Question not found.");

                _mapper.Map(updateDto, existing);
                await _questionRepository.UpdateAsync(existing);

                var dto = _mapper.Map<QuestionResponseDTO>(existing);
                return new ApiResponse<QuestionResponseDTO>(200, dto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<QuestionResponseDTO>(500, $"Error updating question: {ex.Message}");
            }
        }

        public override async Task<ApiResponse<QuestionResponseDTO?>> GetByIdAsync(int id)
        {
            try
            {
                var questions = await _questionRepository.GetAllWithIncludes(new List<string> { "Questionnaire", "AnswerOptions" });
                var question = questions.FirstOrDefault(q => q.Id == id);

                if (question == null)
                    return new ApiResponse<QuestionResponseDTO?>(404, "Question not found.");

                var dto = new QuestionResponseDTO
                {
                    Id = question.Id,
                    QuestionnaireId = question.QuestionnaireId,
                    Text = question.Text,
                    Type = question.Type,
                    QuestionnaireTitle = question.Questionnaire?.Title,
                    AnswerOptions = question.AnswerOptions?
                        .Select(opt => new AnswerOptionDTO
                        {
                            Id = opt.Id,
                            Text = opt.Text

                        }).ToList()
                };

                return new ApiResponse<QuestionResponseDTO?>(200, dto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<QuestionResponseDTO?>(500, $"Error loading question: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<QuestionResponseDTO>>> GetByQuestionnaireIdAsync(int questionnaireId)
        {
            try
            {
                var questions = await _questionRepository.GetByQuestionnaireIdAsync(questionnaireId);

                if (questions == null || !questions.Any())
                    return new ApiResponse<List<QuestionResponseDTO>>(404, "No questions found for this questionnaire.");

                var responseList = questions.Select(q => new QuestionResponseDTO
                {
                    Id = q.Id,
                    Text = q.Text,
                    Type = q.Type,
                    QuestionnaireId = q.QuestionnaireId,
                    QuestionnaireTitle = q.Questionnaire?.Title,
                    AnswerOptions = q.AnswerOptions?
                        .Select(opt => new AnswerOptionDTO
                        {
                            Id = opt.Id,
                            Text = opt.Text
                        }).ToList()
                }).ToList();
                return new ApiResponse<List<QuestionResponseDTO>>(200, responseList);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<QuestionResponseDTO>>(500, $"Error retrieving questions: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<QuestionResponseDTO>>> GetByTypeAsync(string type)
        {
            try
            {
                var questions = await _questionRepository.GetByTypeAsync(type);

                if (questions == null || !questions.Any())
                    return new ApiResponse<List<QuestionResponseDTO>>(404, "No questions found with the specified type.");

                var dtos = questions.Select(q => new QuestionResponseDTO
                {
                    Id = q.Id,
                    QuestionnaireId = q.QuestionnaireId,
                    Text = q.Text,
                    Type = q.Type,
                    QuestionnaireTitle = q.Questionnaire?.Title,
                    AnswerOptions = q.AnswerOptions?
                        .Select(opt => new AnswerOptionDTO
                        {
                            Id = opt.Id,
                            Text = opt.Text
                        }).ToList()
                }).ToList();

                return new ApiResponse<List<QuestionResponseDTO>>(200, dtos);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<QuestionResponseDTO>>(500, $"Error loading questions by type: {ex.Message}");
            }
        }

        public override async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var success =  _questionRepository.DeleteAsync(id);
                if (success == null)
                    return new ApiResponse<bool>(404, "Questionnaire not found.");

                return new ApiResponse<bool>(200, true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(500, $"An error occurred while deleting the questionnaire: {ex.Message}");
            }
        }

        public override async Task<ApiResponse<List<QuestionResponseDTO>>> GetAllAsync()
        {
            try
            {
                var questions = await _questionRepository.GetAllWithIncludes(new List<string> { "Questionnaire", "AnswerOptions" });

                var dtos = questions.Select(q => new QuestionResponseDTO
                {
                    Id = q.Id,
                    QuestionnaireId = q.QuestionnaireId,
                    Text = q.Text,
                    Type = q.Type,
                    QuestionnaireTitle = q.Questionnaire?.Title,
                    AnswerOptions = q.AnswerOptions?.Select(opt => new AnswerOptionDTO
                    {
                        Id = opt.Id,
                        Text = opt.Text
                    }).ToList()
                }).ToList();

                return new ApiResponse<List<QuestionResponseDTO>>(200, dtos);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<QuestionResponseDTO>>(500, $"Error loading questions: {ex.Message}");
            }
        }


    }
}
