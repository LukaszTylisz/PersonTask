using MediatR;

namespace PersonTask.Application.Persons.Commands.DeletePerson;

public class DeletePersonCommand(int id) : IRequest
{
    public int Id { get; } = id;
}