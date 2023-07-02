using TechnicalTest.Application.Common.Exceptions;
using TechnicalTest.Application.Applicants.Commands.CreateApplicant;
using TechnicalTest.Application.Applicants.Commands.UpdateApplicant;
using TechnicalTest.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace TechnicalTest.Application.IntegrationTests.Applicants.Commands;

using static Testing;

public class UpdateApplicantTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidApplicantId()
    {
        var command = new UpdateApplicantCommand { Id = 99, Title = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        var ApplicantId = await SendAsync(new CreateApplicantCommand
        {
            Title = "New List"
        });

        await SendAsync(new CreateApplicantCommand
        {
            Title = "Other List"
        });

        var command = new UpdateApplicantCommand
        {
            Id = ApplicantId,
            Title = "Other List"
        };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Title")))
                .And.Errors["Title"].Should().Contain("The specified title already exists.");
    }

    [Test]
    public async Task ShouldUpdateApplicant()
    {
        var userId = await RunAsDefaultUserAsync();

        var ApplicantId = await SendAsync(new CreateApplicantCommand
        {
            Title = "New List"
        });

        var command = new UpdateApplicantCommand
        {
            Id = ApplicantId,
            Title = "Updated List Title"
        };

        await SendAsync(command);

        var list = await FindAsync<Applicant>(ApplicantId);

        list.Should().NotBeNull();
        list!.Title.Should().Be(command.Title);
        list.LastModifiedBy.Should().NotBeNull();
        list.LastModifiedBy.Should().Be(userId);
        list.LastModified.Should().NotBeNull();
        list.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
