using TechnicalTest.Application.Common.Exceptions;
using TechnicalTest.Application.Steps.Commands.CreateStep;
using TechnicalTest.Application.Steps.Commands.UpdateStep;
using TechnicalTest.Application.Applicants.Commands.CreateApplicant;
using TechnicalTest.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace TechnicalTest.Application.IntegrationTests.Steps.Commands;

using static Testing;

public class UpdateStepTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidStepId()
    {
        var command = new UpdateStepCommand { Id = 99, Title = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateStep()
    {
        var userId = await RunAsDefaultUserAsync();

        var ApplicantId = await SendAsync(new CreateApplicantCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateStepCommand
        {
            ApplicantId = ApplicantId,
            Title = "New Item"
        });

        var command = new UpdateStepCommand
        {
            Id = itemId,
            Title = "Updated Item Title"
        };

        await SendAsync(command);

        var item = await FindAsync<Step>(itemId);

        item.Should().NotBeNull();
        item!.Title.Should().Be(command.Title);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().NotBeNull();
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
