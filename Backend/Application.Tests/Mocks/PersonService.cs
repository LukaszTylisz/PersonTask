using Application.Contracts.Persistence;
using Application.Features.Person.Dto;
using Domain.Entitites;

namespace Application.Tests.Mocks;

public class PersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<List<Person>> GetPersons()
    {
        return (List<Person>)await _personRepository.GetAll();
    }

    public async Task<Person> CreatePerson(Person person)
    {
        await _personRepository.Create(person);
        return person;
    }

    public async Task UpdatePerson(Person person)
    {
        await _personRepository.Update(person);
    }

    public async Task DeletePerson(Person person)
    {
        await _personRepository.Delete(person);
    }
}