using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Enums;

namespace FinalLabProject.Application.Boats.Commands.UpdateBoatDetail;

public record UpdateBoatDetailCommand : IRequest
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}

public class UpdateBoatDetailCommandHandler : IRequestHandler<UpdateBoatDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBoatDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBoatDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Boats
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.ListId = request.ListId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
