using Application.Contracts.Persistence;
using Application.Exceptions;
using MediatR;

namespace Application.Features.Person.Command.Delete;

public class DeletePersonCommandHandler(IPersonRepository repository)
    : IRequestHandler<DeletePersonCommand, Unit>
{
    public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await repository.GetById(request.Id);

        if (person == null)
        {
            throw new NotFoundException(nameof(Person), request.Id);
        }

        await repository.Delete(person);

        return Unit.Value;
    }
}