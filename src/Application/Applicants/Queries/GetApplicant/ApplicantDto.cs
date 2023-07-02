using TechnicalTest.Application.Common.Mappings;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Application.Applicants.Queries.GetApplicants;

public class ApplicantDto : IMapFrom<Applicant>
{
    public ApplicantDto()
    {
        Steps = Array.Empty<StepDto>();
    }

    public int Id { get; init; }

    public string? Title { get; init; }

    public IReadOnlyCollection<StepDto> Steps { get; init; }
}
