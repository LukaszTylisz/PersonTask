namespace Application.Features.Person.Dto;

public class PersonDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<EmailDto> Emails { get; set; } = new List<EmailDto>();
}