using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationContext _context;
        public TestRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Test> AddAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task<List<Test>> GetAllAsync()
        {
            return await _context.Tests.ToListAsync();
        }

        public async Task<Test?> GetByIdAsync(int id)
        {
            return await _context.Tests.FindAsync(id);
        }

        public async Task<List<Test>> GetAllByUserAsync(int userId)
        {
            return await _context.Tests.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<int> SubmitTestAsync(TestResponseDTO input)
        {
            // Validamos que se envíen respuestas
            if (input.Answers == null || !input.Answers.Any())
            {
                throw new ArgumentException("Debes enviar al menos una respuesta");
            }

            // Obtenemos el nombre de la categoría
            var category = await _context.Categories.FindAsync(input.CategoryId);
            if (category == null)
            {
                throw new Exception("Categoría no encontrada.");
            }

            // Obtenemos el nombre del cuestionario
            var questionnaire = await _context.Questionnaires.FindAsync(input.QuestionnaireId);
            if (questionnaire == null)
            {
                throw new Exception("Cuestionario no encontrado.");
            }

            // Convertimos las respuestas en un diccionario para fácil acceso
            string questionnaireName = questionnaire.Title;
            DateTime now = DateTime.Now;
            string fechaActual = now.ToString("yyyyMMdd");
            string fechaHoraStr = now.ToString("dd 'de' MMMM 'de' yyyy, h:mm tt", new System.Globalization.CultureInfo("es-ES"));

            // Buscar tests previos del usuario para generar un nuevo número de intento
            var intentosHoy = await _context.Tests
                .Where(t =>
                    t.UserId == input.UserId &&
                    t.Title.StartsWith($"{questionnaireName} no. {input.UserId}-{fechaActual}"))
                .CountAsync() + 1;

            // Generar título y descripción
            string title = $"{questionnaireName} no. {input.UserId}-{fechaActual}-{intentosHoy}";
            string description = $"{questionnaireName} tomado por el usuario en fecha {fechaHoraStr}. Intento #{intentosHoy}.";

            // Registrar el Test
            var newTest = new Test
            {
                Title = title,
                Description = description,
                CategoryId = input.CategoryId,
                UserId = input.UserId
            };

            // Se guarda el test y se obtiene el ID
            _context.Tests.Add(newTest);
            await _context.SaveChangesAsync();

            return newTest.Id; // este ID se guardará luego en TestResult
        }

    }
}
