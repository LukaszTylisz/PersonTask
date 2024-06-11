using MediatR;
using Microsoft.Extensions.Logging;
using PersonTask.Domain.Entities;
using PersonTask.Domain.Exceptions;
using PersonTask.Domain.Repositories;

namespace PersonTask.Application.Emails.Commands.CreateEmail.DeleteEmails;

public class DeleteEmailsForPersonCommandHandler(
    ILogger<DeleteEmailsForPersonCommandHandler> logger,
    IPersonRepository personRepository,
    IEmailsRepository emailsRepository) : IRequestHandler<DeleteEmailsForPersonCommand>
{
    public async Task Handle(DeleteEmailsForPersonCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Removing all emails from person: {PersonId}", request.PersonId);

        var person = await personRepository.GetByIdAsync(request.PersonId);
        if (person == null) throw new NotFoundException(nameof(Person), request.PersonId.ToString());

        await emailsRepository.Delete(person.Emails);
    }
}