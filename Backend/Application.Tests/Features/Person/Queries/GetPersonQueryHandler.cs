using Application.Contracts.Persistence;
using Application.Tests.Mocks;
using AutoMapper;
using Moq;

namespace Application.Tests.Features.Person.Queries;

public class GetPersonQueryHandler
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly PersonService _personService;

    public GetPersonQueryHandler()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _personService = new PersonService(_personRepositoryMock.Object);
    }
    
    [Fact]
    public async Task GetPersons_ShouldReturnAllPersons()
    {
        var persons = TestDataGenerator.GeneratePersons(5);
        _personRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(persons);
        
        var result = await _personService.GetPersons();
        
        Assert.Equal(5, result.Count);
        _personRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
    }
}