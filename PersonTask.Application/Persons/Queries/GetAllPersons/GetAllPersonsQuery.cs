using MediatR;
using PersonTask.Application.Common;
using PersonTask.Application.Persons.Dtos;
using PersonTask.Domain.Constants;

namespace PersonTask.Application.Persons.Queries.GetAllPersons;

public class GetAllPersonsQuery : IRequest<PagedResult<PersonDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}