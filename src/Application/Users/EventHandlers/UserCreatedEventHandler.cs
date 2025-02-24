using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Domain.Events;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace DukandaCore.Application.Users.EventHandlers;

public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly ILogger<UserCreatedEventHandler> _logger;
    private readonly IEmailService _emailService;
    private readonly IBackgroundJobClient _backgroundJobs;

    public UserCreatedEventHandler(
        ILogger<UserCreatedEventHandler> logger,
        IEmailService emailService,
        IBackgroundJobClient backgroundJobs)
    {
        _logger = logger;
        _emailService = emailService;
        _backgroundJobs = backgroundJobs;
    }

    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Dukanda Domain Event: {DomainEvent}", notification.GetType().Name);

        _backgroundJobs.Enqueue(() => 
            _emailService.SendVerificationEmailAsync(
                notification.User.Email, 
                notification.User.SecurityStamp!));

        return Task.CompletedTask;
    }
}
