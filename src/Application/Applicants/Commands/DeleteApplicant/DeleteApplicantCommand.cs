using TechnicalTest.Application.Common.Exceptions;
using TechnicalTest.Application.Common.Interfaces;
using TechnicalTest.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TechnicalTest.Application.Applicants.Commands.DeleteApplicant;

public record DeleteApplicantCommand(int Id) : IRequest;

public class DeleteApplicantCommandHandler : IRequestHandler<DeleteApplicantCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteApplicantCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteApplicantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Applicants
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Applicant), request.Id);
        }

        _context.Applicants.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
