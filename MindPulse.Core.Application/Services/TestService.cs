using AutoMapper;
using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.DTOs.Evaluations.TestResults;
using MindPulse.Core.Application.DTOs.User.Admin;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Shared.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IQuestionnaireService _questionnaireService;

        public TestService(
            ITestRepository testRepository, 
            IMapper mapper,
            ICategoryService categoryService,
            IQuestionnaireService questionnaireService)
        {
            _testRepository = testRepository;
            _mapper = mapper;
            _categoryService = categoryService;
            _questionnaireService = questionnaireService;
        }

        public async Task<ApiResponse<int>> CreateAsync(TestCreateDTO dto)
        {
            var entity = _mapper.Map<Test>(dto);
            await _testRepository.AddAsync(entity);
            return new ApiResponse<int>(200, entity.Id);
        }

        public async Task<ApiResponse<List<TestDTO>>> GetAllAsync()
        {
            var result = await _testRepository.GetAllAsync();
            var dtos = _mapper.Map<List<TestDTO>>(result);
            return new ApiResponse<List<TestDTO>>(200, dtos);
        }

        public async Task<ApiResponse<TestDTO>> GetByIdAsync(int id)
        {
            var test = await _testRepository.GetByIdAsync(id);
            if (test == null)
                return new ApiResponse<TestDTO>(404, "Test no encontrado");

            var dto = _mapper.Map<TestDTO>(test);
            return new ApiResponse<TestDTO>(200, dto);
        }

        public async Task<ApiResponse<List<TestDTO>>> GetAllByUserAsync(int userId)
        {
            var tests = await _testRepository.GetAllByUserAsync(userId);
            var dtoList = _mapper.Map<List<TestDTO>>(tests);
            return new ApiResponse<List<TestDTO>>(200, dtoList);
        }

        public async Task<ApiResponse<int>> SubmitTestAsync(TestResponseDTO input)
        {
            // Validamos que se envíen respuestas
            if (input.Answers == null || !input.Answers.Any())
            {
                return new ApiResponse<int>(400, "Debes enviar al menos una respuesta");
            }

            // Obtenemos el nombre de la categoría
            var category = await _categoryService.GetByIdAsync(input.CategoryId);
            if (!category.Success || category.Data == null)
            {
                return new ApiResponse<int>(400, "Categoría no encontrada.");
            }

            // Obtenemos el nombre del cuestionario
            var questionnaire = await _questionnaireService.GetByIdAsync(input.QuestionnaireId);
            if (!questionnaire.Success || questionnaire.Data == null)
            {
                return new ApiResponse<int>(400, "Cuestionario no encontrado.");
            }

            // Convertimos las respuestas en un diccionario para fácil acceso
            string questionnaireName = questionnaire.Data.Title;
            DateTime now = DateTime.Now;
            string fechaActual = now.ToString("yyyyMMdd");
            string fechaHoraStr = now.ToString("dd 'de' MMMM 'de' yyyy, h:mm tt", new System.Globalization.CultureInfo("es-ES"));

            // Buscar tests previos del usuario para generar un nuevo número de intento
            var allTestsResult = await GetAllByUserAsync(input.UserId);
            int intentosHoy = allTestsResult.Data
            .Where(t => t.Title.StartsWith($"{questionnaireName} no. {input.UserId}-{fechaActual}"))
            .Count() + 1;

            // Generar título y descripción
            string title = $"{questionnaireName} no. {input.UserId}-{fechaActual}-{intentosHoy}";
            string description = $"{questionnaireName} tomado por el usuario en fecha {fechaHoraStr}. Intento #{intentosHoy}.";

            // Registrar el Test
            var testCreateDto = new TestCreateDTO
            {
                Title = title,
                Description = description,
                CategoryId = input.CategoryId,
                UserId = input.UserId
            };

            // Se guarda el test y se obtiene el ID
            var createdTest = await CreateAsync(testCreateDto);
            int testId = createdTest.Data; // este ID se guardará luego en TestResult

            return createdTest;
        }
    }
}
