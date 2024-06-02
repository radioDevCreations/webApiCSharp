using FinalLabProject.Domain.Events;
using Microsoft.Extensions.Logging;

namespace FinalLabProject.Application.Boats.EventHandlers;

public class BoatCreatedEventHandler : INotificationHandler<BoatCreatedEvent>
{
    private readonly ILogger<BoatCreatedEventHandler> _logger;

    public BoatCreatedEventHandler(ILogger<BoatCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(BoatCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("FinalLabProject Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
