
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements;

internal class CreatedMultipleRestaurantsRequirementHandler(
    
    IUserContext userContext,
    IRestaurantsRepository restaurantsRepository) : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>

{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
    {
        var user = userContext.GetCurrentUser();

       

        var restaurants =await restaurantsRepository.GetAllAsync();
      
        var count  = restaurants.Count(u=>u.OwnerId == user.Id);
        if (count >= requirement.MinmumRestaurantsCreated)
        {
            context.Succeed(requirement);
        }
        else
        {

            context.Fail();
        }

    }
}

