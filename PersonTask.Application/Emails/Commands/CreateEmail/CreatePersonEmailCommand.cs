using MediatR;

namespace PersonTask.Application.Emails.Commands.CreateEmail;

public class CreatePersonEmailCommand : IRequest<int>
{
    public string Email { get; set; } = default!;
    public int PersonId { get; set; }
}