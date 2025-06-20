using AutoMapper;
using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Domain.Entities;

namespace MindPulse.Core.Application.Mappings
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            // User → UserResponseDTO
            CreateMap<User, UserResponseDTO>();

            // UserResponseDTO → User
            CreateMap<UserResponseDTO, User>();

            // UserRegistrationDTO → User (ignora el hash, se hace manual)
            CreateMap<UserRegistrationDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // ChangePasswordDTO → User (mapear solo el hash)
            CreateMap<ChangePasswordDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.NewPassword));
        }
    }
}
