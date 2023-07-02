using TechnicalTest.Application.Common.Interfaces;
using TechnicalTest.Application.Common.Security;
using MediatR;

namespace TechnicalTest.Application.Applicants.Commands.PurgeApplicants;

[Authorize(Roles = "Administrator")]
[Authorize(Policy = "CanPurge")]
public record PurgeApplicantsCommand : IRequest;

public class PurgeApplicantsCommandHandler : IRequestHandler<PurgeApplicantsCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeApplicantsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PurgeApplicantsCommand request, CancellationToken cancellationToken)
    {
        _context.Applicants.RemoveRange(_context.Applicants);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
