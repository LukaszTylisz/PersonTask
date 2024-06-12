using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.Entitites;
using MediatR;

namespace Application.Features.Person.Command.Create;

public class AddPersonCommandHandler(IPersonRepository repository, IMapper mapper) : IRequestHandler<AddPersonCommand, int>
{
    public async Task<int> Handle(AddPersonCommand request, CancellationToken cancellationToken)
    {
        var validator = new AddPersonCommandValidator(repository);
        var validatorResult = await validator.ValidateAsync(request.Person);

        if (validatorResult.Errors.Any())
            throw new BadRequestException("Invalid Person", validatorResult);
        
        var person = mapper.Map<Domain.Entitites.Person>(request.Person);

        foreach (var emailDto in request.Person.Emails)
        {
            if (!person.Emails.Any(e => e.EmailAddress == emailDto.Email))
            {
                var email = mapper.Map<Email>(emailDto);
                email.Person = person;
                person.Emails.Add(email);
            }
        }

        await repository.Create(person);

        return person.Id;
    }
}