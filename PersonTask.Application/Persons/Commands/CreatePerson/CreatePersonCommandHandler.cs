using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonTask.Domain.Entities;
using PersonTask.Domain.Repositories;

namespace PersonTask.Application.Persons.Commands.CreatePerson;

public class CreatePersonCommandHandler(ILogger<CreatePersonCommandHandler> logger,
    IMapper mapper,
    IPersonRepository personRepository) : IRequestHandler<CreatePersonCommand, int>
{
    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = mapper.Map<Person>(request);

        int id = await personRepository.Create(person);
        return id;
    }
}