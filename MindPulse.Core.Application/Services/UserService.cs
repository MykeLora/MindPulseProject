using AutoMapper;
using MindPulse.Core.Application.DTOs.Auth;
using MindPulse.Core.Application.DTOs.User.Admin;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Wrappers;
using MindPulse.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Services
{
    public class UserService : GenericService<UserAdminCreateDTO, UserAdminUpdateDTO, User, UserResponseAdminDTO>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
            : base(repo, mapper)
        {
            _userRepository = repo;
            _mapper = mapper;
        }

        public override async Task<ApiResponse<List<UserResponseAdminDTO>>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var userDtos = _mapper.Map<List<UserResponseAdminDTO>>(users);
            return new ApiResponse<List<UserResponseAdminDTO>>(200, userDtos);
        }

        public override Task<ApiResponse<UserResponseAdminDTO?>> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public override async Task<ApiResponse<UserResponseAdminDTO>> CreateAsync(UserAdminCreateDTO createDto)
        {

            var user = _mapper.Map<User>(createDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createDto.Password);

            var createdUser = await _userRepository.AddAsync(user);

            var responseDto = _mapper.Map<UserResponseAdminDTO>(createdUser);

            return new ApiResponse<UserResponseAdminDTO>(201, responseDto);
        }

        public override async Task<ApiResponse<UserResponseAdminDTO>> UpdateAsync(UserAdminUpdateDTO updateDto)
        {

            var user = await _userRepository.GetByIdAsync(updateDto.Id);
            if (user == null)
            {
                return new ApiResponse<UserResponseAdminDTO>(404, "User not found");
            }

            _mapper.Map(updateDto, user);

            if (!string.IsNullOrWhiteSpace(updateDto.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateDto.Password);
            }

            await _userRepository.UpdateAsync(user);

            var responseDto = _mapper.Map<UserResponseAdminDTO>(user);
            return new ApiResponse<UserResponseAdminDTO>(200, responseDto);
        }

        public override Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            return base.DeleteAsync(id);
        }

    
    }
}
