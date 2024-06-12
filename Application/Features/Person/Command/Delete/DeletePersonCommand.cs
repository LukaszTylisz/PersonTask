using MediatR;

namespace Application.Features.Person.Command.Delete;

public class DeletePersonCommand : IRequest<Unit>
{
    public int Id { get; set; }
}