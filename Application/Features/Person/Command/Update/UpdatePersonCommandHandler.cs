using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.Entitites;
using MediatR;

namespace Application.Features.Person.Command.Update;

public class UpdatePersonCommandHandler(IPersonRepository repository, IMapper mapper)
    : IRequestHandler<UpdatePersonCommand, Unit>
{
    public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateCommandValidator();
        var validatorResult = await validator.ValidateAsync(request.Person);

        if (validatorResult.Errors.Any())
            throw new BadRequestException("Invalid Person", validatorResult);
        
        var person = await repository.GetById(request.Person.Id);

        if (person == null)
        {
            throw new NotFoundException(nameof(Person), request.Person.Id);
        }

        mapper.Map(request.Person, person);

        person.Emails.Clear();

        foreach (var emailDto in request.Person.Emails)
        {
            var email = mapper.Map<Email>(emailDto);
            email.Person = person;
            person.Emails.Add(email);
        }

        await repository.Update(person);

        return Unit.Value;
    }
}