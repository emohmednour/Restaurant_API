using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constant;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(Logger<RestaurantAuthorizationService> logger,
    IUserContext userContext) : IRestaurantAuthorizationService
{

    public bool Authorize(Restaurant restaurant, ResourceOperation operation)
    {

        var user = userContext.GetCurrentUser();

        logger.LogInformation("Authorization user {UserEmail}, to {operation} for restaurant {Restaurantname}",
            user.Email, operation, restaurant.Name);


        //.1  read & create => for any one (no restriction)
        if (operation == ResourceOperation.Create || operation == ResourceOperation.Read)
        {

            logger.LogInformation("Create/Read operation - successful authorization");
            return true;
        }


        //.2  Delete => only Admin
        if (operation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {

            logger.LogInformation("Admin user delete operation - successful authorization");
            return true;
        }

        //.3  Delete & Update => only Owner
        if ((operation == ResourceOperation.Update || operation == ResourceOperation.Delete)
            && user.Id == restaurant.OwnerId)
        {

            logger.LogInformation("Restaurant Owner operation - successful authorization");
            return true;
        }

        return false;
    }
}
