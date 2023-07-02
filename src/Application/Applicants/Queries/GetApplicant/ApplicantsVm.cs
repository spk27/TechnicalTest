namespace TechnicalTest.Application.Applicants.Queries.GetApplicants;

public class ApplicantsVm
{
    public IReadOnlyCollection<PriorityLevelDto> PriorityLevels { get; init; } = Array.Empty<PriorityLevelDto>();

    public IReadOnlyCollection<ApplicantDto> Lists { get; init; } = Array.Empty<ApplicantDto>();
}
