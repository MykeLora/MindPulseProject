using AutoMapper;
using MindPulse.Core.Application.DTOs.AnswerOption;
using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Application.DTOs.Evaluations;
using MindPulse.Core.Application.DTOs.Question;
using MindPulse.Core.Application.DTOs.Questionaries;
using MindPulse.Core.Domain.Entities;
using MindPulse.Core.Domain.Entities.Evaluations;

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
