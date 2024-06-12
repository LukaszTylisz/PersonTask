using Application.Features.Person.Dto;
using MediatR;

namespace Application.Features.Person.Queries.GetById;

public class GetPersonByIdQuery : IRequest<PersonDto>
{
    public int Id { get; set; }
    
    public GetPersonByIdQuery(int id)
    {
        Id = id;
    }
}