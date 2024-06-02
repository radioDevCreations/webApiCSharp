using FinalLabProject.Application.Common.Models;

namespace FinalLabProject.Application.Harbours.Queries.GetTodos;

public class TodosVm
{
    public IReadOnlyCollection<LookupDto> PriorityLevels { get; init; } = Array.Empty<LookupDto>();

    public IReadOnlyCollection<HarbourDto> Lists { get; init; } = Array.Empty<HarbourDto>();
}
