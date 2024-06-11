using MediatR;
using PersonTask.Application.Emails.Dtos;

namespace PersonTask.Application.Emails.Queries.GetEmailsForPerson;

public class GetEmailsForPersonQuery(int personId) : IRequest<IEnumerable<PersonEmailDto>>
{
    public int PersonId { get; } = personId;
}