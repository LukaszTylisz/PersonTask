namespace PersonTask.Domain.Entities;

public class Person
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<PersonEmail> Emails { get; set; } = new();
}