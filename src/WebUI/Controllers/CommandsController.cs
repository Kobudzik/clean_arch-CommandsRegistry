using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Models;
using CommandsRegistry.Application.JsonCommands.Commands.CreateCommandEntry;
using CommandsRegistry.Application.JsonCommands.Commands.DeleteCommandEntry;
using CommandsRegistry.Application.JsonCommands.Commands.UpdateCommandEntry;
using CommandsRegistry.Application.JsonCommands.Queries;
using CommandsRegistry.Application.JsonCommands.Queries.GetJsonCommand;
using CommandsRegistry.Application.JsonCommands.Queries.GetJsonCommands;
using CommandsRegistry.Application.JsonCommands.Queries.GetPaginatedJsonCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandsRegistry.WebUI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CommandsController : ApiControllerBase
    {
        [HttpGet("paginated-list")]
        public async Task<ActionResult<PaginatedList<CommandEntryDto>>> GetList(
            [FromQuery] Pager pager,
            [FromQuery] GetPaginatedJsonCommandsFilterModel filter)
        {
            var query = new GetPaginatedJsonCommandsQuery(pager, filter);
            return await Mediator.Send(query);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<CommandEntryDto>>> GetList()
        {
            var result = await Mediator.Send(new GetJsonCommandsQuery());
            return result.ToList();
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommandEntryDto>> Get([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetJsonCommandQuery(id));
            return result != null ? Ok(result) : NotFound();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] CreateCommandEntryCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateCommandEntryCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCommandEntryCommand(id));
            return Ok();
        }
    }
}