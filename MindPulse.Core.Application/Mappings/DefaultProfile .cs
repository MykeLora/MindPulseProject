using AutoMapper;
using MindPulse.Core.Application.DTOs.AnswerOption;
using MindPulse.Core.Application.DTOs.Auth;
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
            #region User Mappings

            CreateMap<User, UserResponseDTO>();
            CreateMap<UserResponseDTO, User>();

            CreateMap<UserRegistrationDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<ChangePasswordDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.NewPassword));

            #endregion

            #region AnswerOption Mappings

            CreateMap<AnswerOptionCreateDTO, AnswerOption>();
            CreateMap<AnswerOptionUpdateDTO, AnswerOption>();
            CreateMap<AnswerOption, AnswerOptionDTO>();
            CreateMap<AnswerOption, AnswerOptionResponseDTO>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Question.Text));

            #region Question Mappings

            CreateMap<QuestionCreateDTO, Question>();
            CreateMap<QuestionUpdateDTO, Question>();
            CreateMap<Question, QuestionCreateDTO>();
            CreateMap<Question, QuestionResponseDTO>();

            CreateMap<Question, QuestionResponseDTO>()
                .ForMember(dest => dest.QuestionnaireTitle, opt => opt.MapFrom(src => src.Questionnaire.Title));


            #endregion

            #region Questionnaire
            CreateMap<QuestionnaireCreateDTO, Questionnaire>();
            CreateMap<QuestionnaireUpdateDTO, Questionnaire>();
            CreateMap<Questionnaire, QuestionnaireSimpleDTO>();

            CreateMap<Questionnaire, QuestionnaireResponseDTO>()
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));
            #endregion

            #endregion
        }
    }

}
