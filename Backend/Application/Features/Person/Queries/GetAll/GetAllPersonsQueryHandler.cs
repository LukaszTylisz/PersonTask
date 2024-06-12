using Application.Contracts.Persistence;
using Application.Features.Person.Dto;
using AutoMapper;
using MediatR;

namespace Application.Features.Person.Queries.GetAll;

public class GetAllPersonsQueryHandler(IPersonRepository repository, IMapper mapper)
    : IRequestHandler<GetAllPersonsQuery, List<PersonDto>>
{
    public async Task<List<PersonDto>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        var persons = await repository.GetAllWithEmails();
        return mapper.Map<List<PersonDto>>(persons);
    }
}