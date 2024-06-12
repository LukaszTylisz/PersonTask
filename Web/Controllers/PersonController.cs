using Application.Features.Person.Command.Create;
using Application.Features.Person.Command.Delete;
using Application.Features.Person.Command.Update;
using Application.Features.Person.Dto;
using Application.Features.Person.Queries.GetAll;
using Application.Features.Person.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<PersonDto>>> GetAll()
    {
        var persons = await _mediator.Send(new GetAllPersonsQuery());
        return Ok(persons);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> GetById(int id)
    {
        var person = await _mediator.Send(new GetPersonByIdQuery(id));
        if (person == null)
        {
            return NotFound();
        }
        return Ok(person);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> Create(PersonDto personDto)
    {
        var command = new AddPersonCommand { Person = personDto };
        var personId = await _mediator.Send(command);
        return Ok(personId);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, PersonDto personDto)
    {
        if (id != personDto.Id)
        {
            return BadRequest("Invalid ID");
        }

        var command = new UpdatePersonCommand { Person = personDto };
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeletePersonCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
