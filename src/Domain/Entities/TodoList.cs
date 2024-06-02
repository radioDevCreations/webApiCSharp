namespace FinalLabProject.Domain.Entities;

public class Harbour : BaseAuditableEntity
{
    public string? Title { get; set; }

    public Colour Colour { get; set; } = Colour.White;

    public IList<Boat> Items { get; private set; } = new List<Boat>();
}
