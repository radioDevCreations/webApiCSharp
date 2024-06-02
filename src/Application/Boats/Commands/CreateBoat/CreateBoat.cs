using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Events;

namespace FinalLabProject.Application.Boats.Commands.CreateBoat;

public record CreateBoatCommand : IRequest<int>
{
    public int ListId { get; init; }

    public string? Title { get; init; }
}

public class CreateBoatCommandHandler : IRequestHandler<CreateBoatCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateBoatCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBoatCommand request, CancellationToken cancellationToken)
    {
        var entity = new Boat
        {
            ListId = request.ListId,
            Title = request.Title,
            Done = false
        };

        entity.AddDomainEvent(new BoatCreatedEvent(entity));

        _context.Boats.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
