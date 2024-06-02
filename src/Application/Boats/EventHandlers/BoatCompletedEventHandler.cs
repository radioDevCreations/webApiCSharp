using FinalLabProject.Domain.Events;
using Microsoft.Extensions.Logging;

namespace FinalLabProject.Application.Boats.EventHandlers;

public class BoatCompletedEventHandler : INotificationHandler<BoatCompletedEvent>
{
    private readonly ILogger<BoatCompletedEventHandler> _logger;

    public BoatCompletedEventHandler(ILogger<BoatCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(BoatCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("FinalLabProject Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
