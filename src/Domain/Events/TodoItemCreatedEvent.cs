namespace FinalLabProject.Domain.Events;

public class BoatCreatedEvent : BaseEvent
{
    public BoatCreatedEvent(Boat item)
    {
        Item = item;
    }

    public Boat Item { get; }
}
