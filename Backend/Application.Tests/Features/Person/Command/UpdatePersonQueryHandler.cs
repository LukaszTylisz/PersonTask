using Application.Contracts.Persistence;
using Application.Tests.Mocks;
using Moq;

namespace Application.Tests.Features.Person.Command;

public class UpdatePersonQueryHandler
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly PersonService _personService;

    public UpdatePersonQueryHandler()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _personService = new PersonService(_personRepositoryMock.Object);
    }
    
    [Fact]
    public async Task UpdatePerson_ShouldModifyPerson()
    {
        var person = TestDataGenerator.GeneratePersons(1).First();
        _personRepositoryMock.Setup(repo => repo.Update(It.IsAny<Domain.Entitites.Person>())).Returns(Task.CompletedTask);
        
        await _personService.UpdatePerson(person);

        _personRepositoryMock.Verify(repo => repo.Update(person), Times.Once);
    }
}