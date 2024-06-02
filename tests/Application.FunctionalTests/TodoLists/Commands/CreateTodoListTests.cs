using FinalLabProject.Application.Common.Exceptions;
using FinalLabProject.Application.Harbours.Commands.CreateHarbour;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Application.FunctionalTests.Harbours.Commands;

using static Testing;

public class CreateHarbourTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateHarbourCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        await SendAsync(new CreateHarbourCommand
        {
            Title = "Shopping"
        });

        var command = new CreateHarbourCommand
        {
            Title = "Shopping"
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateHarbour()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateHarbourCommand
        {
            Title = "Tasks"
        };

        var id = await SendAsync(command);

        var list = await FindAsync<Harbour>(id);

        list.Should().NotBeNull();
        list!.Title.Should().Be(command.Title);
        list.CreatedBy.Should().Be(userId);
        list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
