using MediatR;
using Microsoft.Extensions.Logging;
using PersonTask.Domain.Entities;
using PersonTask.Domain.Exceptions;
using PersonTask.Domain.Repositories;

namespace PersonTask.Application.Persons.Commands.DeletePerson;

public class DeletePersonCommandHandler(ILogger<DeletePersonCommandHandler> logger,
    IPersonRepository personRepository) : IRequestHandler<DeletePersonCommand>
{
    public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting person with id: {PersonId}", request.Id);
        var person = await personRepository.GetByIdAsync(request.Id);
        if (person is null)
            throw new NotFoundException(nameof(Person), request.Id.ToString());

        await personRepository.Delete(person);
    }
}