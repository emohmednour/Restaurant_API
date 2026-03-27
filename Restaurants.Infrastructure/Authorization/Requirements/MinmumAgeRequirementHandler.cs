
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinmumAgeRequirementHandler(Logger<MinmumAgeRequirementHandler> logger,IUserContext userContext) : AuthorizationHandler<MinmumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinmumAgeRequirement requirement)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("User : {Email} with Date of birth:{BDo}", user.Email, user.DateOfBirth);

        if(user.DateOfBirth == null)
        {
            logger.LogWarning("DateOfBirth in null");
            context.Fail();
            return Task.CompletedTask;
        }
        if (user.DateOfBirth.Value.AddYears(requirement.MinmumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Authorization Successded");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();

        }
        return Task.CompletedTask;


    }
}