using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonTask.Application.Emails.Commands.CreateEmail;
using PersonTask.Application.Emails.Commands.CreateEmail.DeleteEmails;
using PersonTask.Application.Emails.Dtos;
using PersonTask.Application.Emails.Queries.GetEmailByIdForPerson;
using PersonTask.Application.Emails.Queries.GetEmailsForPerson;
using PersonTask.Application.Persons.Queries.GetPersonsById;

namespace PersonTask.API.Controllers;

[Route("api/persons/{personId}/emails")]
[ApiController]
public class EmailsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateEmail([FromRoute] int personId, CreatePersonEmailCommand command)
    {
        command.PersonId = personId;

        var emailId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetByIdForPerson), new { personId, emailId }, null);
    }

    [HttpGet]
    //[Authorize(Policy = PolicyNames.AtLeast20)]
    public async Task<ActionResult<IEnumerable<PersonEmailDto>>> GetAllForPerson([FromRoute] int restaurantId)
    {
        var emails = await mediator.Send(new GetEmailsForPersonQuery(restaurantId));
        return Ok(emails);
    }
    
    [HttpGet("{emailId}")]
    public async Task<ActionResult<PersonEmailDto>> GetByIdForPerson([FromRoute] int personId, [FromRoute]int emailId)
    {
        var dish = await mediator.Send(new GetEmailByIdForPersonQuery(personId, emailId));
        return Ok(dish);
    }
    
    
    [HttpDelete]
    public async Task<IActionResult> DeleteEmailsForPerson([FromRoute] int personId)
    {
        await mediator.Send(new DeleteEmailsForPersonCommand(personId));
        return NoContent();
    }
}