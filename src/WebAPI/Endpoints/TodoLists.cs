using FinalLabProject.Application.Harbours.Commands.CreateHarbour;
using FinalLabProject.Application.Harbours.Commands.DeleteHarbour;
using FinalLabProject.Application.Harbours.Commands.UpdateHarbour;
using FinalLabProject.Application.Harbours.Queries.GetTodos;

namespace FinalLabProject.Web.Endpoints;

public class Harbours : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetHarbours)
            .MapPost(CreateHarbour)
            .MapPut(UpdateHarbour, "{id}")
            .MapDelete(DeleteHarbour, "{id}");
    }

    public Task<TodosVm> GetHarbours(ISender sender)
    {
        return  sender.Send(new GetTodosQuery());
    }

    public Task<int> CreateHarbour(ISender sender, CreateHarbourCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateHarbour(ISender sender, int id, UpdateHarbourCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteHarbour(ISender sender, int id)
    {
        await sender.Send(new DeleteHarbourCommand(id));
        return Results.NoContent();
    }
}
