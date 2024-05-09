using ChiSquares.AuthService.Application.Handlers.Users.Queries;
using Craft.Application.Handlers.Users.Command;
using Craft.Application.Handlers.Users.Quries;
using CRUD.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRUD.API.Controllers;

[Route("api/[controller]")]
[ApiConventionType(typeof(DefaultApiConventions))]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserMiniModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(StatusResponse), 500)]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserMiniModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StatusResponse), 500)]
        public async Task<IActionResult> UpdateUser([FromRoute] long id, [FromBody] UpdateUserCommand command)
        {
            if (command != null)
            {
                command.Id = id;
            }
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetByEmail/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserMiniModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StatusResponse), 500)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByEmail([FromRoute] string email)
        {
            return Ok(await _mediator.Send(new GetUserByEmailQuery(email)));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserMiniModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StatusResponse), 500)]
        public async Task<IActionResult> GetUserById([FromRoute] long id)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery(id)));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(StatusResponse), 201)]
        [ProducesResponseType(typeof(StatusResponse), 400)]
        [ProducesResponseType(typeof(StatusResponse), 500)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var response = await _mediator.Send(new DeleteUserCommand() { Id = id });

            return Ok(response);
        }
}
