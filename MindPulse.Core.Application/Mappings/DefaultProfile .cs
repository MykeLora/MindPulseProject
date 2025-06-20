using AutoMapper;
using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Application.DTOs.Categories;
using MindPulse.Core.Domain.Entities;
using MindPulse.Core.Domain.Entities.Categories;

namespace MindPulse.Core.Application.Mappings
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            // --- USER MAPPINGS ---
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserResponseDTO, User>();

            CreateMap<UserRegistrationDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<ChangePasswordDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.NewPassword));

            // --- CATEGORY MAPPINGS ---
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
