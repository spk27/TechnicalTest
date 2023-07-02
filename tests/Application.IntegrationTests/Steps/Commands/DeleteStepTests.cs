using TechnicalTest.Application.Common.Exceptions;
using TechnicalTest.Application.Steps.Commands.CreateStep;
using TechnicalTest.Application.Steps.Commands.DeleteStep;
using TechnicalTest.Application.Applicants.Commands.CreateApplicant;
using TechnicalTest.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace TechnicalTest.Application.IntegrationTests.Steps.Commands;

using static Testing;

public class DeleteStepTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidStepId()
    {
        var command = new DeleteStepCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteStep()
    {
        var ApplicantId = await SendAsync(new CreateApplicantCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateStepCommand
        {
            ApplicantId = ApplicantId,
            Title = "New Item"
        });

        await SendAsync(new DeleteStepCommand(itemId));

        var item = await FindAsync<Step>(itemId);

        item.Should().BeNull();
    }
}
