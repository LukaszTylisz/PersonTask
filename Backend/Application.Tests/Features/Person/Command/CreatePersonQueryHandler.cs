using Application.Contracts.Persistence;
using Application.Tests.Mocks;
using Moq;

namespace Application.Tests.Features.Person.Command;

public class CreatePersonQueryHandler
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly PersonService _personService;

    public CreatePersonQueryHandler()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _personService = new PersonService(_personRepositoryMock.Object);
    }

    [Fact]
    public async Task CreatePerson_ShouldAddPerson()
    {
        var person = TestDataGenerator.GeneratePersons(3).First();
        _personRepositoryMock.Setup(repo => repo.Create(It.IsAny<Domain.Entitites.Person>()))
            .Returns((Domain.Entitites.Person p) => Task.FromResult(p));
        
        var result = await _personService.CreatePerson(person);
        
        Assert.Equal(person, result);
        _personRepositoryMock.Verify(repo => repo.Create(person), Times.Once);
    }
}