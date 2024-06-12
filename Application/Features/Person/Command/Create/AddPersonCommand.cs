using Application.Features.Person.Dto;
using MediatR;

namespace Application.Features.Person.Command.Create;

public class AddPersonCommand : IRequest<int>
{
    public PersonDto Person { get; set; } = default!;
}