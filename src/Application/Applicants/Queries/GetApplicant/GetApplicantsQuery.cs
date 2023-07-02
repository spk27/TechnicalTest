using AutoMapper;
using AutoMapper.QueryableExtensions;
using TechnicalTest.Application.Common.Interfaces;
using TechnicalTest.Application.Common.Security;
using TechnicalTest.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TechnicalTest.Application.Applicants.Queries.GetApplicants;

[Authorize]
public record GetApplicantsQuery : IRequest<ApplicantsVm>;

public class GetApplicantsQueryHandler : IRequestHandler<GetApplicantsQuery, ApplicantsVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetApplicantsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApplicantsVm> Handle(GetApplicantsQuery request, CancellationToken cancellationToken)
    {
        return new ApplicantsVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                .ToList(),

            Lists = await _context.Applicants
                .AsNoTracking()
                .ProjectTo<ApplicantDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken)
        };
    }
}
