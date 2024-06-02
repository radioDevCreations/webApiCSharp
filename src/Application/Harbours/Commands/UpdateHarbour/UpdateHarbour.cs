using FinalLabProject.Application.Common.Interfaces;

namespace FinalLabProject.Application.Harbours.Commands.UpdateHarbour;

public record UpdateHarbourCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
}

public class UpdateHarbourCommandHandler : IRequestHandler<UpdateHarbourCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateHarbourCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateHarbourCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Harbours
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;

        await _context.SaveChangesAsync(cancellationToken);

    }
}
