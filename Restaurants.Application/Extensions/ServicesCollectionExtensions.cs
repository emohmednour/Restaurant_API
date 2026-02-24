
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
namespace Restaurants.Application.Extensions;

public static class ServicesCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}
