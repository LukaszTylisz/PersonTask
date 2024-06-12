using Domain.Common;

namespace Domain.Entitites;

public class Person : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<Email> Emails { get; set; } = new List<Email>();
}