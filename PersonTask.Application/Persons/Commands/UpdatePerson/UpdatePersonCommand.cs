using MediatR;

namespace PersonTask.Application.Persons.Commands.UpdatePerson;

public class UpdatePersonCommand : IRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Description { get; set; } = default!;
}