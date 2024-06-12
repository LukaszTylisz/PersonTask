using Application.Contracts.Persistence;
using Application.Features.Person.Dto;
using FluentValidation;

namespace Application.Features.Person.Command.Create;

public class AddPersonCommandValidator : AbstractValidator<CreatePersonDto>
{
    private readonly IPersonRepository _repository;
    public AddPersonCommandValidator(IPersonRepository repository)
    {
        RuleFor(dto => dto.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Length(3, 50).WithMessage("First name must be between 3 and 50 characters.");
        
        RuleFor(dto => dto.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Length(3, 50).WithMessage("Last name must be between 3 and 50 characters.");
        
        When(dto => dto != null, () =>
        {
            RuleFor(dto => dto.Emails)
                .Must(emails => emails.All(email => !string.IsNullOrEmpty(email.Email)))
                .WithMessage("Email address cannot be empty.");
        });

        _repository = repository;
    }
}