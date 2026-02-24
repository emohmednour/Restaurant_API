using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
namespace Restaurants.Application.Extensions;

public static class ServicesCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var Assymblies = AppDomain.CurrentDomain.GetAssemblies();
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddAutoMapper(Assymblies);
        services.AddValidatorsFromAssembly(typeof(ServicesCollectionExtensions).Assembly)
            .AddFluentValidationAutoValidation();
    }
}
