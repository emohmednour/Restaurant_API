
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistance;
using Restaurants.Infrastructure.Repositories;

using Restaurants.Infrastructure.Seeders;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.Infrastructure.Extenstions;

public static class ServicesCollectionExtenstions
{


    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDB");
        services.AddDbContext<RestaurantDbContext>(option =>
        {
            option.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging();
        });

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<RestaurantDbContext>();


        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();

        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality, policy =>
            {
                policy.RequireClaim(AppClaimsType.Nationality, "German", "Polish");
            });
            
    }




}
