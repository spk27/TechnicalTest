using TechnicalTest.Application.Common.Exceptions;
using TechnicalTest.Application.Applicants.Commands.CreateApplicant;
using TechnicalTest.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace TechnicalTest.Application.IntegrationTests.Applicants.Commands;

using static Testing;

public class CreateApplicantTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateApplicantCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        await SendAsync(new CreateApplicantCommand
        {
            Title = "Shopping"
        });

        var command = new CreateApplicantCommand
        {
            Title = "Shopping"
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateApplicant()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateApplicantCommand
        {
            Title = "Tasks"
        };

        var id = await SendAsync(command);

        var list = await FindAsync<Applicant>(id);

        list.Should().NotBeNull();
        list!.Title.Should().Be(command.Title);
        list.CreatedBy.Should().Be(userId);
        list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
