
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class CreatedMultipleRestaurantsRequirementHandler(ILogger<CreatedMultipleRestaurantsRequirementHandler> logger,
    IUserContext userContext,IRestaurantsRepository restaurantsRepository) : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("User: {userEmail} with have Restaurants:{Restaurants}"
            , user!.Email, requirement.MinmumRestaurantsCreated);

        var restaurants =await restaurantsRepository.GetAllAsync();
      
        var count  = restaurants.Count(u=>u.OwnerId == user.Id);
        if (count >= requirement.MinmumRestaurantsCreated)
        {
            logger.LogInformation("User has more than 2 restaurant");
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("User has less than 2 restaurant");

            context.Fail();
        }

    }
}

