using AutoMapper;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.Interfaces.Repositories.Recommendations;
using MindPulse.Core.Application.Interfaces.Services.Recommendations;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Services.Recommendations
{
    public class EducationalContentService : GenericService<EducationalContentDTO, EducationalContentDTO, EducationalContent, EducationalContentDTO>, IEducationalContentService
    {
        public EducationalContentService(IEducationalContentRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}
