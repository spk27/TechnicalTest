using TechnicalTest.Application.Common.Interfaces;
using TechnicalTest.Domain.Entities;
using MediatR;

namespace TechnicalTest.Application.Applicants.Commands.CreateApplicant;

public record CreateApplicantCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateApplicantCommandHandler : IRequestHandler<CreateApplicantCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateApplicantCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateApplicantCommand request, CancellationToken cancellationToken)
    {
        var entity = new Applicant();

        entity.Title = request.Title;

        _context.Applicants.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
