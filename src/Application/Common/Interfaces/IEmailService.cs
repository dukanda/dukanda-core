namespace DukandaCore.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendInvitationEmailAsync(string to, string invitedByName);
    Task SendVerificationEmailAsync(string to, string verificationToken);
    Task SendWelcomeEmailAsync(string to, string userName);
}