using FinalLabProject.Application.Common.Interfaces;

namespace FinalLabProject.Application.Boats.Commands.UpdateBoat;

public record UpdateBoatCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}

public class UpdateBoatCommandHandler : IRequestHandler<UpdateBoatCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBoatCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBoatCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Boats
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;
        entity.Done = request.Done;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
