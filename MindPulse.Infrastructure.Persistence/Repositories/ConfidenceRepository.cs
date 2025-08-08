using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Application.DTOs.Confidence;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.Repositories
{
    public class ConfidenceRepository : IConfidenceRepository
    {
        private readonly ApplicationContext _context;

        public ConfidenceRepository(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Hace una búsqueda combinada EmotionalHistories y EmotionAnalyses en memoria, filtrado por UserId.
        /// </summary>
        private IEnumerable<(float Confidence, DateTime? Created)> GetCombinedRecords(int userId)
        {
            // We bring only the necessary fields to reduce memory usage
            var histories = _context.EmotionalHistories
                .Where(h => h.UserId == userId)
                .Select(h => new { h.Confidence, h.Created })
                .AsNoTracking()
                .ToList();

            var analyses = _context.EmotionAnalyses
                .Where(a => a.UserId == userId)
                .Select(a => new { a.Confidence, a.Created })
                .AsNoTracking()
                .ToList();

            // We join them in memory and return a combined list of tuples
            return histories
                .Concat(analyses)
                .Select(x => (x.Confidence, x.Created));
        }


        /// <summary>
        /// Calcula la confianza promedio diaria de un usuario.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Promedio de Confianza Diario</returns>
        public async Task<ConfidenceDailyDTO> GetDailyConfidenceByUserIdAsync(int userId)
        {
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            var records = GetCombinedRecords(userId)
                .Where(r => r.Created >= today && r.Created < tomorrow)
                .ToList();

            if (!records.Any())
                return new ConfidenceDailyDTO
                {
                    AverageConfidence = 0,
                    TotalEntries = 0,
                    Date = today
                };

            return new ConfidenceDailyDTO
            {
                AverageConfidence = records.Average(r => r.Confidence),
                TotalEntries = records.Count,
                Date = today
            };
        }


        /// <summary>
        /// Calcula la confianza promedio semanal de un usuario.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Promedio de Confianza Semanal</returns>
        public async Task<ConfidenceWeeklyDTO> GetWeeklyConfidenceByUserIdAsync(int userId)
        {
            var today = DateTime.UtcNow.Date;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7).AddTicks(-1);

            var records = GetCombinedRecords(userId)
                .Where(r => r.Created >= startOfWeek && r.Created <= endOfWeek)
                .ToList();

            if (!records.Any())
                return new ConfidenceWeeklyDTO
                {
                    AverageConfidence = 0,
                    AverageEntriesPerDay = 0,
                    StartOfWeek = startOfWeek,
                    EndOfWeek = endOfWeek
                };

            return new ConfidenceWeeklyDTO
            {
                AverageConfidence = records.Average(r => r.Confidence),
                AverageEntriesPerDay = records.Count / 7f,
                StartOfWeek = startOfWeek,
                EndOfWeek = endOfWeek
            };
        }

        /// <summary>
        /// Calcula la confianza promedio mensual de un usuario.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Promedio de Confianza Mensual</returns>
        public async Task<ConfidenceMontlyDTO> GetMonthlyConfidenceByUserIdAsync(int userId)
        {
            var today = DateTime.UtcNow.Date;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddTicks(-1);

            var records = GetCombinedRecords(userId)
                .Where(r => r.Created >= startOfMonth && r.Created <= endOfMonth)
                .ToList();

            if (!records.Any())
                return new ConfidenceMontlyDTO
                {
                    AverageConfidence = 0,
                    AverageEntriesPerWeek = 0,
                    Year = today.Year,
                    Month = today.Month
                };

            var totalDays = (endOfMonth - startOfMonth).Days + 1;
            var weeksInMonth = totalDays / 7f;

            return new ConfidenceMontlyDTO
            {
                AverageConfidence = records.Average(r => r.Confidence),
                AverageEntriesPerWeek = records.Count / weeksInMonth,
                Year = today.Year,
                Month = today.Month
            };
        }

        /// <summary>
        /// Calcula la confianza global de un usuario.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Promedio de confianza global</returns>
        public async Task<ConfidenceGlobalDTO> GetGlobalConfidenceByUserIdAsync(int userId)
        {
            var records = GetCombinedRecords(userId).ToList();

            if (!records.Any())
                return new ConfidenceGlobalDTO
                {
                    AverageConfidence = 0,
                    AverageEntriesPerMonth = 0,
                    TotalEntries = 0
                };

            var minDate = records.Min(r => r.Created) ?? DateTime.UtcNow;
            var maxDate = records.Max(r => r.Created) ?? DateTime.UtcNow;

            var totalMonths = (maxDate - minDate).Days / 30f;
            if (totalMonths < 1) totalMonths = 1;

            return new ConfidenceGlobalDTO
            {
                AverageConfidence = records.Average(r => r.Confidence),
                AverageEntriesPerMonth = records.Count / totalMonths,
                TotalEntries = records.Count
            };
        }
    }
}
