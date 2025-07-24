using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MindPulse.Core.Domain.Entities.User;

namespace MindPulse.Core.Application.DTOs.Auth
{
    public class UserUpdateDTO
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        [MinLength(2, ErrorMessage = "UserName must be at least 2 characters.")]
        [MaxLength(50, ErrorMessage = "UserName cannot exceed 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        public DateTime? LastModified { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Must specify the type of user")]
        public RoleType Role { get; set; }

    }
}
