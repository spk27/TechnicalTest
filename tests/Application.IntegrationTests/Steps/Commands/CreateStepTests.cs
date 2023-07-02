using TechnicalTest.Application.Common.Exceptions;
using TechnicalTest.Application.Steps.Commands.CreateStep;
using TechnicalTest.Application.Applicants.Commands.CreateApplicant;
using TechnicalTest.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace TechnicalTest.Application.IntegrationTests.Steps.Commands;

using static Testing;

public class CreateStepTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateStepCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateStep()
    {
        // Arrange
        var userId = await RunAsDefaultUserAsync();

        var applicationId = await SendAsync(new CreateApplicantCommand
        {
            Title = "New List"
        });

        var command = new CreateStepCommand
        {
            ApplicantId = applicationId,
            Title = "Tasks"
        };

        // Act
        var itemId = await SendAsync(command);

        var item = await FindAsync<Step>(itemId);

        // Assert
        item.Should().NotBeNull();
        item!.ApplicantId.Should().Be(command.ApplicantId);
        item.Title.Should().Be(command.Title);
        item.CreatedBy.Should().Be(userId);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
