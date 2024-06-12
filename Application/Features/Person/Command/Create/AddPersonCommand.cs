using Application.Features.Person.Dto;
using MediatR;

namespace Application.Features.Person.Command.Create;

public class AddPersonCommand : IRequest<int>
{
    public CreatePersonDto Person { get; set; } = default!;
}