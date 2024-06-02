using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Application.Harbours.Queries.GetTodos;

public class HarbourDto
{
    public HarbourDto()
    {
        Items = Array.Empty<BoatDto>();
    }

    public int Id { get; init; }

    public string? Title { get; init; }

    public string? Colour { get; init; }

    public IReadOnlyCollection<BoatDto> Items { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Harbour, HarbourDto>();
        }
    }
}
