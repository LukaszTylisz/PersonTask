using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonTask.Application.Emails.Dtos;
using PersonTask.Domain.Entities;
using PersonTask.Domain.Exceptions;
using PersonTask.Domain.Repositories;

namespace PersonTask.Application.Emails.Queries.GetEmailByIdForPerson;

public class GetEmailByIdForPersonQueryHandler(
    ILogger<GetEmailByIdForPersonQueryHandler> logger,
    IPersonRepository personRepository,
    IMapper mapper) : IRequestHandler<GetEmailByIdForPersonQuery, PersonEmailDto>
{
    public async Task<PersonEmailDto> Handle(GetEmailByIdForPersonQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving email: {EmailId}, for person with id: {PersonId}", 
            request.EmailId,
            request.PersonId);

        var person = await personRepository.GetByIdAsync(request.PersonId);

        if (person == null) throw new NotFoundException(nameof(Person), request.PersonId.ToString());

        var email = person.Emails.FirstOrDefault(d => d.Id == request.EmailId);
        if (email == null) throw new NotFoundException(nameof(PersonEmail), request.EmailId.ToString());

        var results = mapper.Map<PersonEmailDto>(email);
        return results;
    }
}