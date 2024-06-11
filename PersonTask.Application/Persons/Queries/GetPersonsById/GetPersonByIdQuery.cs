using MediatR;
using PersonTask.Application.Persons.Dtos;

namespace PersonTask.Application.Persons.Queries.GetPersonsById;

public class GetPersonByIdQuery(int id) : IRequest<PersonDto>
{
    public int Id { get; } = id;
}