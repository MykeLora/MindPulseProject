using MindPulse.Core.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        public EmailSettings EmailSettings { get; }
        Task SendAsync(EmailRequest emailRequest);
    }
}
