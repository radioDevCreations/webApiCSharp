namespace FinalLabProject.Domain.Entities;

public class Boat : BaseAuditableEntity
{
    public int ListId { get; set; }

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
                AddDomainEvent(new BoatCompletedEvent(this));
            }

            _done = value;
        }
    }

    public Harbour List { get; set; } = null!;
}
