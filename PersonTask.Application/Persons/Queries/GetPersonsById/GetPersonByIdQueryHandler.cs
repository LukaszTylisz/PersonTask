using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonTask.Application.Persons.Dtos;
using PersonTask.Domain.Entities;
using PersonTask.Domain.Exceptions;
using PersonTask.Domain.Repositories;

namespace PersonTask.Application.Persons.Queries.GetPersonsById;

public class GetPersonByIdQueryHandler(
    ILogger<GetPersonByIdQueryHandler> logger,
    IPersonRepository personRepository,
    IMapper mapper) : IRequestHandler<GetPersonByIdQuery, PersonDto>
{
    public async Task<PersonDto> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting person {PersonId}", request.Id);
        var person = await personRepository.GetByIdAsync(request.Id)
                     ?? throw new NotFoundException(nameof(Person), request.Id.ToString());

        var personDto = mapper.Map<PersonDto>(person);

        return personDto;
    }
}