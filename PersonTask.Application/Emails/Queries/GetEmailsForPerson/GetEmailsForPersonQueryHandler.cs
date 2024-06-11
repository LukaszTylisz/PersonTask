using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonTask.Application.Emails.Dtos;
using PersonTask.Application.Emails.Queries.GetEmailByIdForPerson;
using PersonTask.Domain.Entities;
using PersonTask.Domain.Exceptions;
using PersonTask.Domain.Repositories;

namespace PersonTask.Application.Emails.Queries.GetEmailsForPerson;

public class GetEmailsForPersonQueryHandler(ILogger<GetEmailByIdForPersonQueryHandler> logger,
    IPersonRepository personRepository,
    IMapper mapper) : IRequestHandler<GetEmailsForPersonQuery, IEnumerable<PersonEmailDto>>
{
    public async Task<IEnumerable<PersonEmailDto>> Handle(GetEmailsForPersonQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retreiving emails for person with id: {PersonId}", request.PersonId);
        var person = await personRepository.GetByIdAsync(request.PersonId);

        if (person == null) throw new NotFoundException(nameof(Person), request.PersonId.ToString());

        var results = mapper.Map<IEnumerable<PersonEmailDto>>(person.Emails);

        return results;
    }
}