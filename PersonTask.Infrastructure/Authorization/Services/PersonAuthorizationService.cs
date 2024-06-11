using Microsoft.Extensions.Logging;
using PersonTask.Application.Users;
using PersonTask.Domain.Constants;
using PersonTask.Domain.Entities;
using PersonTask.Domain.Interfaces;

namespace PersonTask.Infrastructure.Authorization.Services;

public class PersonAuthorizationService(
    ILogger<PersonAuthorizationService> logger,
    IUserContext userContext) : IPersonAuthorizationService
{
    public bool Authorize(Person person, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Authorizing user {UserEmail}, to {Operation}",
            user.Email,
            resourceOperation);

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/read operation - successful authorization");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin user, delete operation - successful authorization");
            return true;
        }

        return false;
    }
}