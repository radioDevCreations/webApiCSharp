using FinalLabProject.Application.Common.Exceptions;
using FinalLabProject.Application.Common.Security;
using FinalLabProject.Application.Harbours.Commands.CreateHarbour;
using FinalLabProject.Application.Harbours.Commands.PurgeHarbours;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Application.FunctionalTests.Harbours.Commands;

using static Testing;

public class PurgeHarboursTests : BaseTestFixture
{
    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var command = new PurgeHarboursCommand();

        command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

        var action = () => SendAsync(command);

        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldDenyNonAdministrator()
    {
        await RunAsDefaultUserAsync();

        var command = new PurgeHarboursCommand();

        var action = () => SendAsync(command);

        await action.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task ShouldAllowAdministrator()
    {
        await RunAsAdministratorAsync();

        var command = new PurgeHarboursCommand();

        var action = () => SendAsync(command);

        await action.Should().NotThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task ShouldDeleteAllLists()
    {
        await RunAsAdministratorAsync();

        await SendAsync(new CreateHarbourCommand
        {
            Title = "New List #1"
        });

        await SendAsync(new CreateHarbourCommand
        {
            Title = "New List #2"
        });

        await SendAsync(new CreateHarbourCommand
        {
            Title = "New List #3"
        });

        await SendAsync(new PurgeHarboursCommand());

        var count = await CountAsync<Harbour>();

        count.Should().Be(0);
    }
}
