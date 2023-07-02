using TechnicalTest.Application.Common.Models;
using TechnicalTest.Application.Steps.Commands.CreateStep;
using TechnicalTest.Application.Steps.Commands.DeleteStep;
using TechnicalTest.Application.Steps.Commands.UpdateStep;
using TechnicalTest.Application.Steps.Commands.UpdateStepDetail;
using TechnicalTest.Application.Steps.Queries.GetStepsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TechnicalTest.WebUI.Controllers;

[Authorize]
public class StepsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<StepBriefDto>>> GetStepsWithPagination([FromQuery] GetStepsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateStepCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateStepCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("[action]")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateItemDetails(int id, UpdateStepDetailCommand command)
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
        await Mediator.Send(new DeleteStepCommand(id));

        return NoContent();
    }
}
