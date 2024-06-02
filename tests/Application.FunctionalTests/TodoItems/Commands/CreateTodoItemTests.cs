using FinalLabProject.Application.Common.Exceptions;
using FinalLabProject.Application.Boats.Commands.CreateBoat;
using FinalLabProject.Application.Harbours.Commands.CreateHarbour;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Application.FunctionalTests.Boats.Commands;

using static Testing;

public class CreateBoatTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateBoatCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateBoat()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateHarbourCommand
        {
            Title = "New List"
        });

        var command = new CreateBoatCommand
        {
            ListId = listId,
            Title = "Tasks"
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<Boat>(itemId);

        item.Should().NotBeNull();
        item!.ListId.Should().Be(command.ListId);
        item.Title.Should().Be(command.Title);
        item.CreatedBy.Should().Be(userId);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
