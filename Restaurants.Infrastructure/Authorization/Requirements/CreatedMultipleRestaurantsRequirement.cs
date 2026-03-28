
using Microsoft.AspNetCore.Authorization;
namespace Restaurants.Infrastructure.Authorization.Requirements;

public class CreatedMultipleRestaurantsRequirement(int minmumRestaurantsCreated) : IAuthorizationRequirement
{
    public int MinmumRestaurantsCreated { get; } = minmumRestaurantsCreated;

}

