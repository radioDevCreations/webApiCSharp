namespace FinalLabProject.Domain.Events;

public class BoatCompletedEvent : BaseEvent
{
    public BoatCompletedEvent(Boat item)
    {
        Item = item;
    }

    public Boat Item { get; }
}
