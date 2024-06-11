using FluentValidation;

namespace PersonTask.Application.Persons.Commands.UpdatePerson;

public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .Length(3, 50);
        
        RuleFor(c => c.LastName)
            .Length(3, 50);
    }
}