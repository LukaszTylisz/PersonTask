using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonTask.Domain.Entities;
using PersonTask.Domain.Exceptions;
using PersonTask.Domain.Repositories;

namespace PersonTask.Application.Persons.Commands.UpdatePerson;

public class UpdatePersonCommandHandler(
    ILogger<UpdatePersonCommandHandler> logger,
    IPersonRepository personRepository,
    IMapper mapper) : IRequestHandler<UpdatePersonCommand>
{
    public async Task Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating person with id: {PersonId} with {@UpdatePerson}", request.Id, request);
        var person = await personRepository.GetByIdAsync(request.Id);

        if (person is null)
            throw new NotFoundException(nameof(Person), request.Id.ToString());

        mapper.Map(request, person);

        await personRepository.SaveChanges();
    }
}