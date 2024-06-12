namespace Application.Features.Person.Dto;
public class CreatePersonDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<CreateEmailDto> Emails { get; set; } = new List<CreateEmailDto>();
}
