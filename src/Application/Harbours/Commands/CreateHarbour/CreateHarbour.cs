using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Application.Harbours.Commands.CreateHarbour;

public record CreateHarbourCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateHarbourCommandHandler : IRequestHandler<CreateHarbourCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateHarbourCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateHarbourCommand request, CancellationToken cancellationToken)
    {
        var entity = new Harbour();

        entity.Title = request.Title;

        _context.Harbours.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
