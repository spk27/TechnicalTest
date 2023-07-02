namespace TechnicalTest.Domain.Entities;

public class Step : BaseAuditableEntity
{
    public int ApplicantId { get; set; }

    public string? Title { get; set; }

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTime? Reminder { get; set; }

    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            if (value && !_done)
            {
                AddDomainEvent(new StepCompletedEvent(this));
            }

            _done = value;
        }
    }

    public Applicant Applicant { get; set; } = null!;
}
