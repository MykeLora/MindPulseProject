using AutoMapper;
using MindPulse.Core.Application.DTOs.AnswerOption;
using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Application.DTOs.Categories;
using MindPulse.Core.Application.DTOs.Evaluations.AiResponse;
using MindPulse.Core.Application.DTOs.Evaluations.Test;
using MindPulse.Core.Application.DTOs.Evaluations.TestResults;
using MindPulse.Core.Application.DTOs.Evaluations.UserResponse;
using MindPulse.Core.Application.DTOs.Question;
using MindPulse.Core.Application.DTOs.Questionaries;
using MindPulse.Core.Application.DTOs.Recommendations;
using MindPulse.Core.Application.DTOs.User.Admin;
using MindPulse.Core.Domain.Entities;
using MindPulse.Core.Domain.Entities.Categories;
using MindPulse.Core.Domain.Entities.Evaluations;
using MindPulse.Core.Domain.Entities.Recommendations;
using UserResponseDTO = MindPulse.Core.Application.DTOs.Auth.UserResponseDTO;

namespace MindPulse.Core.Application.Mappings
{

    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            #region UserResponse Mappings
            CreateMap<UserResponse, UserResponsesDTO>().ReverseMap();
            CreateMap<UserResponseCreateDTO, UserResponse>();
            #endregion

            #region AiResponse Mappings
            CreateMap<AiResponse, AiResponseDTO>().ReverseMap();
            CreateMap<AiResponseCreateDTO, AiResponse>();
            #endregion

            #region User Mappings

            CreateMap<User, UserResponseDTO>();
            CreateMap<UserResponseDTO, User>();


            CreateMap<UserAdminCreateDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<User, UserResponseAdminDTO>();


            CreateMap<UserAdminUpdateDTO, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<UserRegistrationDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<ChangePasswordDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.NewPassword));

            #endregion

            #region Category Mappings

            CreateMap<Category, CategoryDTO>().ReverseMap();

            #endregion

            #region Recommendation
            CreateMap<RecommendationCreateDTO, Recommendation>();
            CreateMap<Recommendation, RecommendationDTO>().ReverseMap();
            #endregion

            #region EducationalContent
            CreateMap<EducationalContentCreateDTO, EducationalContent>();
            CreateMap<EducationalContent, EducationalContentDTO>().ReverseMap();
            #endregion

            #region Test Mappings
            CreateMap<TestCreateDTO, Test>();
            CreateMap<Test, TestDTO>();
            CreateMap<TestResponseItemDTO, UserResponse>()
                .ForMember(dest => dest.AnswerOptionId, opt => opt.MapFrom(src => src.AnswerOptionId))
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId));
            #endregion

            #region TestResult Mappings
            CreateMap<TestResultCreateDTO, TestResult>();
            CreateMap<TestResult, TestResultDTO>();
            #endregion

            #region AnswerOption Mappings
            CreateMap<AnswerOptionCreateDTO, AnswerOption>();
            CreateMap<AnswerOptionUpdateDTO, AnswerOption>();
            CreateMap<AnswerOption, AnswerOptionDTO>();
            CreateMap<AnswerOption, AnswerOptionResponseDTO>();
            #endregion

            #region Question Mappings
            CreateMap<QuestionCreateDTO, Question>();
            CreateMap<QuestionUpdateDTO, Question>();
            CreateMap<Question, QuestionCreateDTO>();
            CreateMap<Question, QuestionResponseDTO>();

            CreateMap<Question, QuestionResponseDTO>()
                .ForMember(dest => dest.QuestionnaireTitle, opt => opt.MapFrom(src => src.Questionnaire.Title));
            #endregion

            #region Questionnaire Mappings
            CreateMap<QuestionnaireCreateDTO, Questionnaire>();
            CreateMap<QuestionnaireUpdateDTO, Questionnaire>();
            CreateMap<Questionnaire, QuestionnaireSimpleDTO>();

            CreateMap<Questionnaire, QuestionnaireResponseDTO>()
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));
            #endregion
        }
    }

}
