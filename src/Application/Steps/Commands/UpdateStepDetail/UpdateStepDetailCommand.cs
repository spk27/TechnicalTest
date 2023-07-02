using TechnicalTest.Application.Common.Exceptions;
using TechnicalTest.Application.Common.Interfaces;
using TechnicalTest.Domain.Entities;
using TechnicalTest.Domain.Enums;
using MediatR;

namespace TechnicalTest.Application.Steps.Commands.UpdateStepDetail;

public record UpdateStepDetailCommand : IRequest
{
    public int Id { get; init; }

    public int ApplicantId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}

public class UpdateStepDetailCommandHandler : IRequestHandler<UpdateStepDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateStepDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateStepDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Steps
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Step), request.Id);
        }

        entity.ApplicantId = request.ApplicantId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
