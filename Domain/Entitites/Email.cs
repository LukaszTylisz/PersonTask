using Domain.Common;

namespace Domain.Entitites;

public class Email : BaseEntity
{
    public string EmailAddress { get; set; }  = default!;
    public Person Person { get; set; }  = default!;
    public int PersonId { get; set; }  = default!;
}