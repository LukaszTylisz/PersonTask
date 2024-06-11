using PersonTask.Domain.Entities;

namespace PersonTask.Application.Persons.Dtos;

public class PersonDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Description { get; set; } = default!;

    public List<PersonEmail> Emails { get; set; } = [];
}