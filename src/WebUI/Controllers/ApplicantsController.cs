using TechnicalTest.Application.Applicants.Commands.CreateApplicant;
using TechnicalTest.Application.Applicants.Commands.DeleteApplicant;
using TechnicalTest.Application.Applicants.Commands.UpdateApplicant;
using TechnicalTest.Application.Applicants.Queries.ExportApplicants;
using TechnicalTest.Application.Applicants.Queries.GetApplicants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalTest.WebUI.Controllers;

[Authorize]
public class ApplicantsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApplicantsVm>> Get()
    {
        return await Mediator.Send(new GetApplicantsQuery());
    }

    [HttpGet("{id}")]
    public async Task<FileResult> Get(int id)
    {
        var vm = await Mediator.Send(new ExportApplicantsQuery { ApplicantId = id });

        return File(vm.Content, vm.ContentType, vm.FileName);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateApplicantCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateApplicantCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteApplicantCommand(id));

        return NoContent();
    }
}
