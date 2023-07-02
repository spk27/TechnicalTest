using TechnicalTest.Application.Applicants.Queries.GetApplicants;
using TechnicalTest.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace TechnicalTest.Application.IntegrationTests.Applicants.Queries;

using static Testing;

public class GetApplicantsTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPriorityLevels()
    {
        await RunAsDefaultUserAsync();

        var query = new GetApplicantsQuery();

        var result = await SendAsync(query);

        result.PriorityLevels.Should().NotBeEmpty();
    }

    [Test]
    public async Task ShouldReturnAllListsAndItems()
    {
        await RunAsDefaultUserAsync();

        await AddAsync(new Applicant
        {
            Title = "Shopping",
            Steps =
                    {
                        new Step { Title = "Apples", Done = true },
                        new Step { Title = "Milk", Done = true },
                        new Step { Title = "Bread", Done = true },
                        new Step { Title = "Toilet paper" },
                        new Step { Title = "Pasta" },
                        new Step { Title = "Tissues" },
                        new Step { Title = "Tuna" }
                    }
        });

        var query = new GetApplicantsQuery();

        var result = await SendAsync(query);

        result.Lists.Should().HaveCount(1);
        result.Lists.First().Steps.Should().HaveCount(7);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var query = new GetApplicantsQuery();

        var action = () => SendAsync(query);
        
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
