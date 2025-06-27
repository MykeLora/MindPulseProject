using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.DTOs.User
{
    public class ResetPasswordDTO
    {
        /// <summary>
        /// Gets or sets the email address of the user requesting a password reset.
        /// </summary>
        [Required(ErrorMessage = "A valid Email address is required.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the unique token for password reset.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the new password to be set after reset.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirmation of the new password.
        /// </summary>
        public string ConfirmNewPassword { get; set; }
    }
}
