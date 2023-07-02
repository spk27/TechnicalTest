using TechnicalTest.Application.Common.Interfaces;
using TechnicalTest.Domain.Entities;
using TechnicalTest.Domain.Events;
using MediatR;

namespace TechnicalTest.Application.Steps.Commands.CreateStep;

public record CreateStepCommand : IRequest<int>
{
    public int ApplicantId { get; init; }

    public string? Title { get; init; }
}

public class CreateStepCommandHandler : IRequestHandler<CreateStepCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateStepCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateStepCommand request, CancellationToken cancellationToken)
    {
        var entity = new Step
        {
            ApplicantId = request.ApplicantId,
            Title = request.Title,
            Done = false
        };

        entity.AddDomainEvent(new StepCreatedEvent(entity));

        _context.Steps.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
