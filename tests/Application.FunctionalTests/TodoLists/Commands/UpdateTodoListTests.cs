using FinalLabProject.Application.Common.Exceptions;
using FinalLabProject.Application.Harbours.Commands.CreateHarbour;
using FinalLabProject.Application.Harbours.Commands.UpdateHarbour;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Application.FunctionalTests.Harbours.Commands;

using static Testing;

public class UpdateHarbourTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidHarbourId()
    {
        var command = new UpdateHarbourCommand { Id = 99, Title = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        var listId = await SendAsync(new CreateHarbourCommand
        {
            Title = "New List"
        });

        await SendAsync(new CreateHarbourCommand
        {
            Title = "Other List"
        });

        var command = new UpdateHarbourCommand
        {
            Id = listId,
            Title = "Other List"
        };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Title")))
                .And.Errors["Title"].Should().Contain("'Title' must be unique.");
    }

    [Test]
    public async Task ShouldUpdateHarbour()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateHarbourCommand
        {
            Title = "New List"
        });

        var command = new UpdateHarbourCommand
        {
            Id = listId,
            Title = "Updated List Title"
        };

        await SendAsync(command);

        var list = await FindAsync<Harbour>(listId);

        list.Should().NotBeNull();
        list!.Title.Should().Be(command.Title);
        list.LastModifiedBy.Should().NotBeNull();
        list.LastModifiedBy.Should().Be(userId);
        list.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
