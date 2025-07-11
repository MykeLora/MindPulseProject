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
            #region UserResponse Mappings
            CreateMap<UserResponse, UserResponsesDTO>().ReverseMap();
            CreateMap<UserResponseCreateDTO, UserResponse>();
            #endregion

            #region User Mappings

            CreateMap<User, UserResponseDTO>();
            CreateMap<UserResponseDTO, User>();

            CreateMap<UserRegistrationDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<ChangePasswordDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.NewPassword));

            #endregion

            #region Category Mappings

            CreateMap<Category, CategoryDTO>().ReverseMap();

            #endregion

            #region Recommendation
            CreateMap<Recommendation, RecommendationDTO>().ReverseMap();
            #endregion

            #region EducationalContent
            CreateMap<EducationalContent, EducationalContentDTO>().ReverseMap();
            #endregion

            //#region Test Mappings
            //CreateMap<TestCreateDTO, Test>();
            //CreateMap<TestUpdateDTO, Test>();

            //CreateMap<Test, TestResponseDTO>()
            //    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            //    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            //    .ForMember(dest => dest.QuestionCount, opt => opt.MapFrom(src => src.Questions.Count))
            //    .ForMember(dest => dest.TestResultsCount, opt => opt.MapFrom(src => src.TestResults.Count));
            //#endregion

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
