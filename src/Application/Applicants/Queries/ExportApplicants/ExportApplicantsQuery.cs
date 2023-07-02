using AutoMapper;
using AutoMapper.QueryableExtensions;
using TechnicalTest.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TechnicalTest.Application.Applicants.Queries.ExportApplicants;

public record ExportApplicantsQuery : IRequest<ExportApplicantsVm>
{
    public int ApplicantId { get; init; }
}

public class ExportApplicantsQueryHandler : IRequestHandler<ExportApplicantsQuery, ExportApplicantsVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _fileBuilder;

    public ExportApplicantsQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder fileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _fileBuilder = fileBuilder;
    }

    public async Task<ExportApplicantsVm> Handle(ExportApplicantsQuery request, CancellationToken cancellationToken)
    {
        var records = await _context.Steps
                .Where(t => t.ApplicantId == request.ApplicantId)
                .ProjectTo<StepRecord>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

        var vm = new ExportApplicantsVm(
            "Steps.csv",
            "text/csv",
            _fileBuilder.BuildStepsFile(records));

        return vm;
    }
}
