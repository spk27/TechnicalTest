using TechnicalTest.Application.Common.Mappings;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Application.Common.Models;

// Note: This is currently just used to demonstrate applying multiple IMapFrom attributes.
public class LookupDto : IMapFrom<Applicant>, IMapFrom<Step>
{
    public int Id { get; init; }

    public string? Title { get; init; }
}
