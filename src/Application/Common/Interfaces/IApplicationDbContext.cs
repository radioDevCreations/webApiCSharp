using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Harbour> Harbours { get; }

    DbSet<Boat> Boats { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
