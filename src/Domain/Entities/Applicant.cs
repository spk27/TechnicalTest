namespace TechnicalTest.Domain.Entities;

public class Applicant : BaseAuditableEntity
{
    public string? Title { get; set; }
    

    public IList<Step> Steps { get; private set; } = new List<Step>();
}
