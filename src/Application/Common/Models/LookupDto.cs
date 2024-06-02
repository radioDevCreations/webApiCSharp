using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string? Title { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Harbour, LookupDto>();
            CreateMap<Boat, LookupDto>();
        }
    }
}
