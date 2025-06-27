using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.User
{
    public class ForgotPasswordRequestDTO
    {
        /// <summary>
        /// Gets or sets the email address of the user requesting a password reset.
        /// </summary>
        [Required(ErrorMessage = "A valid Email address is required.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or stes the password reset link.
        /// </summary>
        [Required]
        public string? ResetUrl { get; set; }
    }
}
