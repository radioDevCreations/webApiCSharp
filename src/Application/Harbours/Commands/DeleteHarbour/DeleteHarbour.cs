using FinalLabProject.Application.Common.Interfaces;

namespace FinalLabProject.Application.Harbours.Commands.DeleteHarbour;

public record DeleteHarbourCommand(int Id) : IRequest;

public class DeleteHarbourCommandHandler : IRequestHandler<DeleteHarbourCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteHarbourCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteHarbourCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Harbours
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Harbours.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
