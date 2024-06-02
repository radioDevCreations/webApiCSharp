namespace FinalLabProject.Domain.Events;

public class BoatDeletedEvent : BaseEvent
{
    public BoatDeletedEvent(Boat item)
    {
        Item = item;
    }

    public Boat Item { get; }
}
