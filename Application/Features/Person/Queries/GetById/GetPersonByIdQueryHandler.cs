using Application.Contracts.Persistence;
using Application.Features.Person.Dto;
using AutoMapper;
using MediatR;

namespace Application.Features.Person.Queries.GetById;

public class GetPersonByIdQueryHandler(IPersonRepository repository, IMapper mapper) : IRequestHandler<GetPersonByIdQuery, PersonDto>
{
    public async Task<PersonDto?> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var person = await repository.GetByIdWithEmails(request.Id);
        return mapper.Map<PersonDto?>(person);
    }
}