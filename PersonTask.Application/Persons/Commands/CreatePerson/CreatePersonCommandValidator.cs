using FluentValidation;

namespace PersonTask.Application.Persons.Commands.CreatePerson;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(dto => dto.FirstName)
            .Length(3, 50);
        RuleFor(dto => dto.LastName)
            .Length(3, 50);
    }
}