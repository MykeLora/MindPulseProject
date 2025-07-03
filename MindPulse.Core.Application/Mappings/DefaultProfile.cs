using AutoMapper;
using MindPulse.Core.Application.DTOs.AnswerOption;
using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Application.DTOs.Categories;
using MindPulse.Core.Application.DTOs.Evaluations;
using MindPulse.Core.Application.DTOs.Question;
using MindPulse.Core.Application.DTOs.Questionaries;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Domain.Entities;
using MindPulse.Core.Domain.Entities.Categories;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Core.Domain.Entities.Recommendations;

namespace MindPulse.Core.Application.Mappings
{

    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<UserResponse, UserResponsesDTO>().ReverseMap();
            CreateMap<UserResponseCreateDTO, UserResponse>();
        }
    }

}
