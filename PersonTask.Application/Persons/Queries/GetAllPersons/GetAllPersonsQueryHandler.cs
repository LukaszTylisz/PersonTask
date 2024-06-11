using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PersonTask.Application.Common;
using PersonTask.Application.Persons.Dtos;
using PersonTask.Domain.Repositories;

namespace PersonTask.Application.Persons.Queries.GetAllPersons;

public class GetAllPersonsQueryHandler(
    ILogger<GetAllPersonsQueryHandler> logger,
    IMapper mapper,
    IPersonRepository personRepository) : IRequestHandler<GetAllPersonsQuery, PagedResult<PersonDto>>
{
    public async Task<PagedResult<PersonDto>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all persons");
        var (persons, totalCount) = await personRepository.GetAllMatchingAsync(request.SearchPhrase,
            request.PageSize,
            request.PageNumber,
            request.SortBy,
            request.SortDirection);

        var personsDtos = mapper.Map<IEnumerable<PersonDto>>(persons);

        var result = new PagedResult<PersonDto>(personsDtos, totalCount, request.PageSize, request.PageNumber);

        return result;
    }
}