

namespace FinalLabProject.Application.FunctionalTests.Boats.Commands;

using FinalLabProject.Application.Boats.Commands.CreateBoat;
using FinalLabProject.Application.Boats.Commands.UpdateBoat;
using FinalLabProject.Application.Boats.Commands.UpdateBoatDetail;
using FinalLabProject.Application.Harbours.Commands.CreateHarbour;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Enums;
using static Testing;

public class UpdateBoatDetailTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidBoatId()
    {
        var command = new UpdateBoatCommand { Id = 99, Title = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateBoat()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateHarbourCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateBoatCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        var command = new UpdateBoatDetailCommand
        {
            Id = itemId,
            ListId = listId,
            Note = "This is the note.",
            Priority = PriorityLevel.High
        };

        await SendAsync(command);

        var item = await FindAsync<Boat>(itemId);

        item.Should().NotBeNull();
        item!.ListId.Should().Be(command.ListId);
        item.Note.Should().Be(command.Note);
        item.Priority.Should().Be(command.Priority);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
