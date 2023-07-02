using TechnicalTest.Application.Common.Exceptions;
using TechnicalTest.Application.Applicants.Commands.CreateApplicant;
using TechnicalTest.Application.Applicants.Commands.DeleteApplicant;
using TechnicalTest.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace TechnicalTest.Application.IntegrationTests.Applicants.Commands;

using static Testing;

public class DeleteApplicantTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidApplicantId()
    {
        var command = new DeleteApplicantCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteApplicant()
    {
        var ApplicantId = await SendAsync(new CreateApplicantCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteApplicantCommand(ApplicantId));

        var list = await FindAsync<Applicant>(ApplicantId);

        list.Should().BeNull();
    }
}
