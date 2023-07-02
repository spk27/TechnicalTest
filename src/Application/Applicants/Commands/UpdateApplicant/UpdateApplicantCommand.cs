using TechnicalTest.Application.Common.Exceptions;
using TechnicalTest.Application.Common.Interfaces;
using TechnicalTest.Domain.Entities;
using MediatR;

namespace TechnicalTest.Application.Applicants.Commands.UpdateApplicant;

public record UpdateApplicantCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
}

public class UpdateApplicantCommandHandler : IRequestHandler<UpdateApplicantCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateApplicantCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateApplicantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Applicants
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Applicant), request.Id);
        }

        entity.Title = request.Title;

        await _context.SaveChangesAsync(cancellationToken);

    }
}
