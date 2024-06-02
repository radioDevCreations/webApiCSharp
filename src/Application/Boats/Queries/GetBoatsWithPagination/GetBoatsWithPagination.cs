using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Application.Common.Mappings;
using FinalLabProject.Application.Common.Models;

namespace FinalLabProject.Application.Boats.Queries.GetBoatsWithPagination;

public record GetBoatsWithPaginationQuery : IRequest<PaginatedList<BoatBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetBoatsWithPaginationQueryHandler : IRequestHandler<GetBoatsWithPaginationQuery, PaginatedList<BoatBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBoatsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BoatBriefDto>> Handle(GetBoatsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Boats
            .Where(x => x.ListId == request.ListId)
            .OrderBy(x => x.Title)
            .ProjectTo<BoatBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
