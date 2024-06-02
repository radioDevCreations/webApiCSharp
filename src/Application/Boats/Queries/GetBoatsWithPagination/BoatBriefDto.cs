using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Application.Boats.Queries.GetBoatsWithPagination;

public class BoatBriefDto
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Boat, BoatBriefDto>();
        }
    }
}
