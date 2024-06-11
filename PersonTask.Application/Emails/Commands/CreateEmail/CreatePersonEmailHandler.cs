using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonTask.Domain.Entities;
using PersonTask.Domain.Exceptions;
using PersonTask.Domain.Repositories;

namespace PersonTask.Application.Emails.Commands.CreateEmail;

public class CreatePersonEmailHandler(
    ILogger<CreatePersonEmailHandler> logger,
    IPersonRepository personRepository,
    IEmailsRepository emailsRepository,
    IMapper mapper) : IRequestHandler<CreatePersonEmailCommand, int>
{
    public async Task<int> Handle(CreatePersonEmailCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new email: {@}EmailRequest", request);
        var person = await personRepository.GetByIdAsync(request.PersonId);
        if (person == null) throw new NotFoundException(nameof(Person), request.PersonId.ToString());

        var email = mapper.Map<PersonEmail>(request);

        return await emailsRepository.Create(email);
    }
}