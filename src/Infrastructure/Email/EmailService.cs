using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DukandaCore.Application.Common.Interfaces;

namespace DukandaCore.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public EmailService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        private string LoadTemplate(string templatePath)
        {
            var fullPath = Path.Combine(_environment.ContentRootPath, "wwwroot", "templates", templatePath);
            return File.ReadAllText(fullPath);
        }

        public async Task SendInvitationEmailAsync(string to, string invitedByName)
        {
            var templateData = new Dictionary<string, string>
            {
                { "InvitedByName", invitedByName },
                { "InvitationLink", $"{_configuration["Email:AppUrl"]}" }
            };

            var htmlTemplate = LoadTemplate("invite.html");

            // Substitui placeholders no template
            foreach (var item in templateData)
            {
                htmlTemplate = htmlTemplate.Replace($"{{{item.Key}}}", item.Value);
            }

            await SendEmailAsync(to, "Você foi convidado para participar deste CVM", htmlTemplate);
        }

        public async Task SendVerificationEmailAsync(string to, string verificationToken)
        {
            var templateData = new Dictionary<string, string>
            {
                { "VerificationLink", $"{_configuration["Email:VerificationUrl"]}/{verificationToken}" }
            };

            var htmlTemplate = LoadTemplate("verification.html");
            foreach (var item in templateData)
            {
                htmlTemplate = htmlTemplate.Replace($"{{{item.Key}}}", item.Value);
            }

            await SendEmailAsync(to, "Verifique seu email - Dukanda", htmlTemplate);
        }

        public async Task SendWelcomeEmailAsync(string to, string userName)
        {
            var templateData = new Dictionary<string, string>
            {
                { "UserName", userName },
                { "LoginLink", _configuration["Email:AppUrl"]! }
            };

            var htmlTemplate = LoadTemplate("welcome.html");
            foreach (var item in templateData)
            {
                htmlTemplate = htmlTemplate.Replace($"{{{item.Key}}}", item.Value);
            }

            await SendEmailAsync(to, "Bem-vindo à Dukanda", htmlTemplate);
        }

        private async Task SendEmailAsync(string to, string subject, string htmlBody)
        {
            var fromEmail = _configuration["Email:From"]!;
            var smtpServer = _configuration["Email:SmtpServer"];
            var smtpPort = int.Parse(_configuration["Email:Port"] ?? "587");
            var smtpUser = _configuration["Email:Username"];
            var smtpPass = _configuration["Email:Password"];
            var fromName = _configuration["Email:FromName"] ?? "Dukanda";

            using var smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPass);
            smtpClient.EnableSsl = true;
            using var message = new MailMessage();
            message.From = new MailAddress(fromEmail, fromName);
            message.Subject = subject;
            message.Body = htmlBody;
            message.IsBodyHtml = true;
            message.To.Add(new MailAddress(to));
            await smtpClient.SendMailAsync(message);
        }
    }
}
