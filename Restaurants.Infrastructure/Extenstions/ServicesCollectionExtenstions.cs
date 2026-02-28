
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistance;
using Restaurants.Infrastructure.Repositories;

using Restaurants.Infrastructure.Seeders;
using Restaurants.Domain.Repositories;

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


        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();
    }




}
