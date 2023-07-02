using TechnicalTest.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TechnicalTest.Application.Steps.EventHandlers;

public class StepCompletedEventHandler : INotificationHandler<StepCompletedEvent>
{
    private readonly ILogger<StepCompletedEventHandler> _logger;

    public StepCompletedEventHandler(ILogger<StepCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(StepCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("TechnicalTest Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
