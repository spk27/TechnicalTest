using AutoMapper;
using TechnicalTest.Application.Common.Mappings;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Application.Applicants.Queries.GetApplicants;

public class StepDto : IMapFrom<Step>
{
    public int Id { get; init; }

    public int ApplicantId { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }

    public int Priority { get; init; }

    public string? Note { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Step, StepDto>()
            .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));
    }
}
