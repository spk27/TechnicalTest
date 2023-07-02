namespace TechnicalTest.Domain.Events;

public class StepCreatedEvent : BaseEvent
{
    public StepCreatedEvent(Step item)
    {
        Item = item;
    }

    public Step Item { get; }
}
