using MediatR;

namespace PersonTask.Application.Persons.Commands.CreatePerson;

public class CreatePersonCommand : IRequest<int>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Description { get; set; } = default!;
}