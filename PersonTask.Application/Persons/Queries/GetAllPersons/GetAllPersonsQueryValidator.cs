using FluentValidation;
using PersonTask.Domain.Entities;

namespace PersonTask.Application.Persons.Queries.GetAllPersons;

public class GetAllPersonsQueryValidator : AbstractValidator<GetAllPersonsQuery>
{
    private int[] allowPageSizes = [5, 10, 15, 30];
    private string[] allowedSortByColumnNames = [nameof(Person.FirstName),
        nameof(Person.LastName),
        nameof(Person.Description)];

    public GetAllPersonsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
    }
}