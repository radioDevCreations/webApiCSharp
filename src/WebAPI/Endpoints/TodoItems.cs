using FinalLabProject.Application.Boats.Commands.CreateBoat;
using FinalLabProject.Application.Boats.Commands.UpdateBoat;
using FinalLabProject.Application.Boats.Commands.UpdateBoatDetail;
using FinalLabProject.Application.Boats.Queries.GetBoatsWithPagination;
using FinalLabProject.Application.Common.Models;
using FinalLabProject.Application.Boats.Commands.DeleteBoat;

namespace FinalLabProject.Web.Endpoints;

public class Boats : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetBoatsWithPagination)
            .MapPost(CreateBoat)
            .MapPut(UpdateBoat, "{id}")
            .MapPut(UpdateBoatDetail, "UpdateDetail/{id}")
            .MapDelete(DeleteBoat, "{id}");
    }

    public Task<PaginatedList<BoatBriefDto>> GetBoatsWithPagination(ISender sender, [AsParameters] GetBoatsWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreateBoat(ISender sender, CreateBoatCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateBoat(ISender sender, int id, UpdateBoatCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> UpdateBoatDetail(ISender sender, int id, UpdateBoatDetailCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteBoat(ISender sender, int id)
    {
        await sender.Send(new DeleteBoatCommand(id));
        return Results.NoContent();
    }
}
