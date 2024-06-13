using Application.Contracts.Persistence;
using Application.Tests.Mocks;
using Moq;

namespace Application.Tests.Features.Person.Command;

public class DeletePersonQueryHandler
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly PersonService _personService;

    public DeletePersonQueryHandler()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _personService = new PersonService(_personRepositoryMock.Object);
    }

    [Fact]
    public async Task DeletePerson_ShouldRemovePerson()
    {
        var personId = 1;
        var personToDelete = new Domain.Entitites.Person { Id = personId };
        
        _personRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>()))
            .ReturnsAsync(personToDelete);
        
        _personRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Domain.Entitites.Person>()))
            .Returns(Task.CompletedTask);
        
        await _personService.DeletePerson(personToDelete);
        
        _personRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Domain.Entitites.Person>()), Times.Once);
    }
}