using TechnicalTest.Application.Common.Exceptions;
using TechnicalTest.Application.Steps.Commands.CreateStep;
using TechnicalTest.Application.Steps.Commands.UpdateStep;
using TechnicalTest.Application.Steps.Commands.UpdateStepDetail;
using TechnicalTest.Application.Applicants.Commands.CreateApplicant;
using TechnicalTest.Domain.Entities;
using TechnicalTest.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace TechnicalTest.Application.IntegrationTests.Steps.Commands;

using static Testing;

public class UpdateStepDetailTests : BaseTestFixture
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

        var command = new UpdateStepDetailCommand
        {
            Id = itemId,
            ApplicantId = ApplicantId,
            Note = "This is the note.",
            Priority = PriorityLevel.High
        };

        await SendAsync(command);

        var item = await FindAsync<Step>(itemId);

        item.Should().NotBeNull();
        item!.ApplicantId.Should().Be(command.ApplicantId);
        item.Note.Should().Be(command.Note);
        item.Priority.Should().Be(command.Priority);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().NotBeNull();
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
