using TechnicalTest.Application.Common.Exceptions;
using TechnicalTest.Application.Common.Interfaces;
using TechnicalTest.Domain.Entities;
using TechnicalTest.Domain.Events;
using MediatR;

namespace TechnicalTest.Application.Steps.Commands.DeleteStep;

public record DeleteStepCommand(int Id) : IRequest;

public class DeleteStepCommandHandler : IRequestHandler<DeleteStepCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteStepCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteStepCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Steps
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Step), request.Id);
        }

        _context.Steps.Remove(entity);

        entity.AddDomainEvent(new StepDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }

}
