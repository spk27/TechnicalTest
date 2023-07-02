using TechnicalTest.Application.Common.Exceptions;
using TechnicalTest.Application.Common.Security;
using TechnicalTest.Application.Applicants.Commands.CreateApplicant;
using TechnicalTest.Application.Applicants.Commands.PurgeApplicants;
using TechnicalTest.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace TechnicalTest.Application.IntegrationTests.Applicants.Commands;

using static Testing;

public class PurgeApplicantsTests : BaseTestFixture
{
    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var command = new PurgeApplicantsCommand();

        command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

        var action = () => SendAsync(command);

        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldDenyNonAdministrator()
    {
        await RunAsDefaultUserAsync();

        var command = new PurgeApplicantsCommand();

        var action = () => SendAsync(command);

        await action.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task ShouldAllowAdministrator()
    {
        await RunAsAdministratorAsync();

        var command = new PurgeApplicantsCommand();

        var action = () => SendAsync(command);

        await action.Should().NotThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task ShouldDeleteAllLists()
    {
        await RunAsAdministratorAsync();

        await SendAsync(new CreateApplicantCommand
        {
            Title = "New List #1"
        });

        await SendAsync(new CreateApplicantCommand
        {
            Title = "New List #2"
        });

        await SendAsync(new CreateApplicantCommand
        {
            Title = "New List #3"
        });

        await SendAsync(new PurgeApplicantsCommand());

        var count = await CountAsync<Applicant>();

        count.Should().Be(0);
    }
}
