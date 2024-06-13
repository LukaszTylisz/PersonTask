using Application.Contracts.Persistence;
using Bogus;
using Moq;
using Person = Domain.Entitites.Person;

namespace Application.Tests.Mocks;

public static class TestDataGenerator
{
    public static List<Person> GeneratePersons(int count)
    {
        var personsGenerator = new Faker<Domain.Entitites.Person>()
            .RuleFor(p => p.FirstName, f => f.Person.FirstName)
            .RuleFor(p => p.LastName, f => f.Person.LastName)
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            .RuleFor(p => p.Emails, f => new List<Domain.Entitites.Email>
            {
                new Domain.Entitites.Email
                {
                    EmailAddress = f.Internet.Email()
                }
            });

        return personsGenerator.Generate(count);
    }
}