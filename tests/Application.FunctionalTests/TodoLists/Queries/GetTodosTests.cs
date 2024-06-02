using FinalLabProject.Application.Harbours.Queries.GetTodos;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.ValueObjects;

namespace FinalLabProject.Application.FunctionalTests.Harbours.Queries;

using static Testing;

public class GetTodosTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPriorityLevels()
    {
        await RunAsDefaultUserAsync();

        var query = new GetTodosQuery();

        var result = await SendAsync(query);

        result.PriorityLevels.Should().NotBeEmpty();
    }

    [Test]
    public async Task ShouldReturnAllListsAndItems()
    {
        await RunAsDefaultUserAsync();

        await AddAsync(new Harbour
        {
            Title = "Shopping",
            Colour = Colour.Blue,
            Items =
                    {
                        new Boat { Title = "Apples", Done = true },
                        new Boat { Title = "Milk", Done = true },
                        new Boat { Title = "Bread", Done = true },
                        new Boat { Title = "Toilet paper" },
                        new Boat { Title = "Pasta" },
                        new Boat { Title = "Tissues" },
                        new Boat { Title = "Tuna" }
                    }
        });

        var query = new GetTodosQuery();

        var result = await SendAsync(query);

        result.Lists.Should().HaveCount(1);
        result.Lists.First().Items.Should().HaveCount(7);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var query = new GetTodosQuery();

        var action = () => SendAsync(query);
        
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
