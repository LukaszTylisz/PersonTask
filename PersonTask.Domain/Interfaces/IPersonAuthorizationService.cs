using PersonTask.Domain.Constants;
using PersonTask.Domain.Entities;

namespace PersonTask.Domain.Interfaces;
public interface IPersonAuthorizationService
{
    bool Authorize(Person person, ResourceOperation resourceOperation);
}
