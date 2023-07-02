using TechnicalTest.Application.Common.Exceptions;
using TechnicalTest.Application.Common.Interfaces;
using TechnicalTest.Domain.Entities;
using MediatR;

namespace TechnicalTest.Application.Steps.Commands.UpdateStep;

public record UpdateStepCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}

public class UpdateStepCommandHandler : IRequestHandler<UpdateStepCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateStepCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateStepCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Steps
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Step), request.Id);
        }

        entity.Title = request.Title;
        entity.Done = request.Done;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
