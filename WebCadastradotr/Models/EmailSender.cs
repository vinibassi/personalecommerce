using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebCadastrador.Models
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> logger;

        public EmailSender(ILogger<EmailSender> logger) => this.logger = logger;
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            logger.LogInformation($"Received email to {email} about {subject}. Body:\n{htmlMessage}");
            return Task.CompletedTask;
        }
    }
}
