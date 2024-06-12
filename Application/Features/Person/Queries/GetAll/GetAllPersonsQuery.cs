using Application.Features.Person.Dto;
using MediatR;

namespace Application.Features.Person.Queries.GetAll;

public class GetAllPersonsQuery : IRequest<List<PersonDto>>
{
}