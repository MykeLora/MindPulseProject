using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Application.DTOs.Evaluations.Analysis;
using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.DTOs.Evaluations.TestResults;
using MindPulse.Core.Application.DTOs.Evaluations.UserResponse;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Interfaces.Services.Orchestrations;
using MindPulse.Core.Application.Interfaces.Services.Recommendations;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Categories;
using MindPulse.Core.Domain.Entities.Emotions;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Infrastructure.Persistence.Context;
using MindPulse.Infrastructure.Shared.Services.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Shared.Services.Orchestrations
{
    public class TestOrchestrationService : ITestOrchestrationService
    {
        private readonly ApplicationContext _context;
        private readonly ITestService _testService;
        private readonly IEvaluationService _evaluationService;
        private readonly IQuestionService _questionService;
        private readonly IAnswerOptionService _answerOptionService;
        private readonly ICategoryService _categoryService;
        private readonly IQuestionnaireService _questionnaireService;
        private readonly IOpenAiService _openAiService;
        private readonly IEducationalContentService _educationalContentService;
        private readonly IRecommendationService _recommendationService;
        private readonly ITestResultService _testResultService;
        private readonly IUserResponseService _userResponseService;
        private readonly IMapper _mapper;

        public TestOrchestrationService(
            ApplicationContext context,
            ITestService testService,
            IEvaluationService evaluationService,
            IQuestionService questionService,
            IAnswerOptionService answerOptionService,
            ICategoryService categoryService,
            IQuestionnaireService questionnaireService,
            IOpenAiService openAiService,
            IEducationalContentService educationalContentService,
            IRecommendationService recommendationService,
            ITestResultService testResultService,
            IUserResponseService userResponseService,
            IMapper mapper)
        {
            _context = context;
            _testService = testService;
            _evaluationService = evaluationService;
            _questionService = questionService;
            _answerOptionService = answerOptionService;
            _categoryService = categoryService;
            _questionnaireService = questionnaireService;
            _openAiService = openAiService;
            _educationalContentService = educationalContentService;
            _recommendationService = recommendationService;
            _testResultService = testResultService;
            _userResponseService = userResponseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Evalua un test realizado por el usuario, almacena los resultados y devuelve un análisis detallado.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Resultado del análisis emocional</returns>
        public async Task<ApiResponse<TestAnalysisDTO>> AnalyzeAndStoreTestAsync(TestResponseDTO input)
        {
            // Step 1: Store the test and get the test ID
            var testResponses = await _testService.SubmitTestAsync(input);
            var testId = testResponses.Data;

            // Step 2: Sending the responses, building the prompt for OpenAI and analyzing the test
            var aiAnalysis = await _evaluationService.EvaluateTestAsync(input);

            // Step 3: Registering the confidence level in EmotionalHistories
            await _context.EmotionalHistories.AddAsync(new EmotionalHistory
            {
                Confidence = aiAnalysis.Confidence,
                Summary = aiAnalysis.Summary,
                UserId = input.UserId
            });

            // Step 4: Storing the test result
            var testResults = await _testResultService.CreateAsync(new TestResultCreateDTO
            {
                Summary = aiAnalysis.Summary,
                Confidence = aiAnalysis.Confidence,
                QuestionnaireId = input.QuestionnaireId,
                UserId = input.UserId,
                TestId = testId
            });

            var testResultId = testResults.Data;

            // Step 5: Obtaining Category Name and Storing the educational content
            var category = await _categoryService.GetByIdAsync(input.CategoryId);
            var contentId = await StoreEducationalContentIfValid(aiAnalysis, category.Data.Name, input.CategoryId);

            // Step 6: Storing the recommendation from OpenAI
            await StoreRecommendation(aiAnalysis, input.UserId, input.CategoryId, contentId);

            // Step 7: Storing the user responses
            foreach (var answer in input.Answers)
            {
                await _userResponseService.CreateAsync(new UserResponseCreateDTO
                {
                    TestResultId = testResultId,
                    QuestionId = answer.QuestionId,
                    AnswerOptionId = answer.AnswerOptionId,
                    FreeResponse = null, // FreeResponse is not used in this context
                    UserId = input.UserId
                });
            }

            // Step 8: Returning the analysis details
            return new ApiResponse<TestAnalysisDTO>(200, new TestAnalysisDTO
            {
                Summary = aiAnalysis.Summary,
                Confidence = aiAnalysis.Confidence,
                Recommendation = aiAnalysis.Recommendation,
                Resource = new EducationalContentSnippetDTO
                {
                    Title = aiAnalysis.Resource.Title,
                    Description = aiAnalysis.Resource.Description,
                    Url = aiAnalysis.Resource.Url
                }
            });
        }

        private async Task<int?> StoreEducationalContentIfValid(TestAnalysisDTO aiAnalysis, string category, int categoryId)
        {
            // Verifying if the AI analysis has a valid URL
            if (string.IsNullOrWhiteSpace(aiAnalysis.Resource.Url) || aiAnalysis.Resource.Url == "null")
                return null;

            // Storing the educational content
            var result = await _educationalContentService.CreateAsync(new EducationalContentCreateDTO
            {
                Title = aiAnalysis.Resource.Title,
                Type = category,
                Description = aiAnalysis.Resource.Description,
                Url = aiAnalysis.Resource.Url,
                CategoryId = categoryId
            });

            // Returning the ID of the created educational content if successful
            return result.Success ? result.Data : null;
        }

        private async Task<int?> StoreRecommendation(TestAnalysisDTO aiAnalysis, int userId, int categoryId, int? contentId)
        {
            if (string.IsNullOrWhiteSpace(aiAnalysis.Recommendation)) return null;

            // Converting the current date and time to the required format
            DateTime now = DateTime.Now;
            string fechaActual = now.ToString("yyyyMMdd");
            string fechaHoraStr = now.ToString("dd 'de' MMMM 'de' yyyy, h:mm tt", new System.Globalization.CultureInfo("es-ES"));

            // Counting existing recommendations for the user in the current category
            var recommendationCount = await _context.Recommendations
                .Where(r =>
                    r.UserId == userId &&
                    r.Title.StartsWith($"Recomendación no. {userId}-{categoryId}-{fechaActual}"))
                .CountAsync() + 1;

            // Generating title depending on the count of recommendations
            string title = $"Recomendación no. {userId}-{categoryId}-{fechaActual}-{recommendationCount}";

            // Creating the recommendation
            var result = await _recommendationService.CreateAsync(new RecommendationCreateDTO
            {
                Title = title,
                Content = aiAnalysis.Recommendation,
                UserId = userId,
                CategoryId = categoryId,
                EducationalContentId = contentId
            });

            // Returning the ID of the created recommendation if successful
            return result.Success ? result.Data : null;
        }
    }
}
