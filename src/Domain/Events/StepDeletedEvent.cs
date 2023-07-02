namespace TechnicalTest.Domain.Events;

public class StepDeletedEvent : BaseEvent
{
    public StepDeletedEvent(Step item)
    {
        Item = item;
    }

    public Step Item { get; }
}
