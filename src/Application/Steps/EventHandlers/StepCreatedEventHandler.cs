using TechnicalTest.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TechnicalTest.Application.Steps.EventHandlers;

public class StepCreatedEventHandler : INotificationHandler<StepCreatedEvent>
{
    private readonly ILogger<StepCreatedEventHandler> _logger;

    public StepCreatedEventHandler(ILogger<StepCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(StepCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("TechnicalTest Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
