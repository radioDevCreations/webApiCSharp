using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Events;

namespace FinalLabProject.Application.Boats.Commands.DeleteBoat;

public record DeleteBoatCommand(int Id) : IRequest;

public class DeleteBoatCommandHandler : IRequestHandler<DeleteBoatCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteBoatCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBoatCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Boats
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Boats.Remove(entity);

        entity.AddDomainEvent(new BoatDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }

}
