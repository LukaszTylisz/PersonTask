using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Entitites;
using MediatR;

namespace Application.Features.Person.Command.Create;

public class AddPersonCommandHandler(IPersonRepository repository, IMapper mapper) : IRequestHandler<AddPersonCommand, int>
{
    public async Task<int> Handle(AddPersonCommand request, CancellationToken cancellationToken)
    {
        var person = mapper.Map<Domain.Entitites.Person>(request.Person);

        foreach (var emailDto in request.Person.Emails)
        {
            var email = mapper.Map<Email>(emailDto);
            email.Person = person;
            person.Emails.Add(email);
        }

        await repository.Create(person);

        return person.Id;
    }
}