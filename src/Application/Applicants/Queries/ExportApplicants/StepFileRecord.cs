using TechnicalTest.Application.Common.Mappings;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Application.Applicants.Queries.ExportApplicants;

public class StepRecord : IMapFrom<Step>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
