using Application.Features.Person.Dto;
using MediatR;

namespace Application.Features.Person.Command.Update;

public class UpdatePersonCommand : IRequest<Unit>
{
    public PersonDto Person { get; set; } = default!;
}