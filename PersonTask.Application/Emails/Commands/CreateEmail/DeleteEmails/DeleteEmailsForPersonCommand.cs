using MediatR;

namespace PersonTask.Application.Emails.Commands.CreateEmail.DeleteEmails;

public class DeleteEmailsForPersonCommand(int personId) : IRequest
{
    public int PersonId { get; } = personId;
}