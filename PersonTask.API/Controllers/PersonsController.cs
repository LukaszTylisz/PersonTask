using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonTask.Application.Persons.Commands.CreatePerson;
using PersonTask.Application.Persons.Commands.DeletePerson;
using PersonTask.Application.Persons.Commands.UpdatePerson;
using PersonTask.Application.Persons.Dtos;
using PersonTask.Application.Persons.Queries.GetAllPersons;
using PersonTask.Application.Persons.Queries.GetPersonsById;
using PersonTask.Domain.Constants;

namespace PersonTask.API.Controllers;

[ApiController]
[Route("api/persons")]
[Authorize]
public class PersonsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll([FromQuery] GetAllPersonsQuery query)
    {
        var persons = await mediator.Send(query);
        return Ok(persons);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto?>> GetById([FromRoute] int id)
    {
        var person = await mediator.Send(new GetPersonByIdQuery(id));
        return Ok(person);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePerson([FromRoute] int id, UpdatePersonCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePerson([FromRoute] int id)
    {
        await mediator.Send(new DeletePersonCommand(id));

        return NoContent();
    }

    [HttpPost]
    //[Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> CreatePerson(CreatePersonCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
}