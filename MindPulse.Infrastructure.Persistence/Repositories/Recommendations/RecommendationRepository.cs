using Microsoft.EntityFrameworkCore;
using MindPulse.Core.Application.Interfaces.Repositories.Recommendations;
using MindPulse.Core.Domain.Entities.Recommendations;
using MindPulse.Infrastructure.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindPulse.Infrastructure.Persistence.Repositories.Recommendations
{
    public class RecommendationRepository : GenericRepository<Recommendation>, IRecommendationRepository
    {
        private readonly ApplicationContext _context;  // almacena el contexto localmente

        public RecommendationRepository(ApplicationContext context) : base(context)
        {
            _context = context;  // asigna el contexto recibido
        }




    }
}
