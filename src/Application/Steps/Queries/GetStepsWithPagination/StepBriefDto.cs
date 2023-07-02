using TechnicalTest.Application.Common.Mappings;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Application.Steps.Queries.GetStepsWithPagination;

public class StepBriefDto : IMapFrom<Step>
{
    public int Id { get; init; }

    public int ApplicantId { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}
