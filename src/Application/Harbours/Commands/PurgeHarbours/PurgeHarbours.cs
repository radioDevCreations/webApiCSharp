using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Application.Common.Security;
using FinalLabProject.Domain.Constants;

namespace FinalLabProject.Application.Harbours.Commands.PurgeHarbours;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanPurge)]
public record PurgeHarboursCommand : IRequest;

public class PurgeHarboursCommandHandler : IRequestHandler<PurgeHarboursCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeHarboursCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PurgeHarboursCommand request, CancellationToken cancellationToken)
    {
        _context.Harbours.RemoveRange(_context.Harbours);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
