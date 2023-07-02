namespace TechnicalTest.Domain.Events;

public class StepCompletedEvent : BaseEvent
{
    public StepCompletedEvent(Step item)
    {
        Item = item;
    }

    public Step Item { get; }
}
