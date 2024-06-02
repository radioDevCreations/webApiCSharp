using FinalLabProject.Application.Boats.Commands.CreateBoat;
using FinalLabProject.Application.Boats.Commands.DeleteBoat;
using FinalLabProject.Application.Harbours.Commands.CreateHarbour;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Application.FunctionalTests.Boats.Commands;

using static Testing;

public class DeleteBoatTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidBoatId()
    {
        var command = new DeleteBoatCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteBoat()
    {
        var listId = await SendAsync(new CreateHarbourCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateBoatCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        await SendAsync(new DeleteBoatCommand(itemId));

        var item = await FindAsync<Boat>(itemId);

        item.Should().BeNull();
    }
}
