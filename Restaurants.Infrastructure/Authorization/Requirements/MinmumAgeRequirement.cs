
using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements;

    public class MinmumAgeRequirement(int minmumAge) : IAuthorizationRequirement
    {
        public int MinmumAge{get;} = minmumAge;
    }

