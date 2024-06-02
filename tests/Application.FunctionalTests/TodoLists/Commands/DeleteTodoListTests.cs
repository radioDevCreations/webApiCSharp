using FinalLabProject.Application.Harbours.Commands.CreateHarbour;
using FinalLabProject.Application.Harbours.Commands.DeleteHarbour;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Application.FunctionalTests.Harbours.Commands;

using static Testing;

public class DeleteHarbourTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidHarbourId()
    {
        var command = new DeleteHarbourCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteHarbour()
    {
        var listId = await SendAsync(new CreateHarbourCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteHarbourCommand(listId));

        var list = await FindAsync<Harbour>(listId);

        list.Should().BeNull();
    }
}
