using MediatR;
using PersonTask.Application.Emails.Dtos;

namespace PersonTask.Application.Emails.Queries.GetEmailByIdForPerson;

public class GetEmailByIdForPersonQuery(int personId, int emailId) : IRequest<PersonEmailDto>
{
    public int PersonId { get; } = personId;
    public int EmailId { get; } = emailId;
}