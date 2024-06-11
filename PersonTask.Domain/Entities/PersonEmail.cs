namespace PersonTask.Domain.Entities;

public class PersonEmail
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    //public Person Person { get; set; } = default!;
    public int PersonId { get; set; }
}