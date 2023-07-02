using AutoMapper;
using AutoMapper.QueryableExtensions;
using TechnicalTest.Application.Common.Interfaces;
using TechnicalTest.Application.Common.Mappings;
using TechnicalTest.Application.Common.Models;
using MediatR;

namespace TechnicalTest.Application.Steps.Queries.GetStepsWithPagination;

public record GetStepsWithPaginationQuery : IRequest<PaginatedList<StepBriefDto>>
{
    public int ApplicantId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetStepsWithPaginationQueryHandler : IRequestHandler<GetStepsWithPaginationQuery, PaginatedList<StepBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStepsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<StepBriefDto>> Handle(GetStepsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Steps
            .Where(x => x.ApplicantId == request.ApplicantId)
            .OrderBy(x => x.Title)
            .ProjectTo<StepBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
