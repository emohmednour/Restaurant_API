using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using AutoMapper;


namespace Restaurants.Application.Restaurants;

internal class RestaurantService(IRestaurantRepository resturantRepository,
     ILogger<RestaurantService> logger,IMapper mapper) : IRestaurantService
{
    public async Task<int> Create(CreateRestaurantDto dto)
    {
        logger.LogInformation("Creating restaurant ");
        var restaurant= mapper.Map<Restaurant>(dto);
        var Id = await resturantRepository.Create(restaurant);
        return Id; 
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllResturants()
    {
        logger.LogInformation("Getting All restaurants");
        var restaurants = await resturantRepository.GetAllAsync();
        var restaurantsDTO = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return restaurantsDTO!;
    }

    public async Task<RestaurantDto?> GetRestaurant(int Id)
    {
        logger.LogInformation(message: $"Getting restaurant {Id}");

        var restaurant = await resturantRepository.GetAsync(Id);
        var restaurantDTO = mapper.Map<RestaurantDto>(restaurant);

        return restaurantDTO;
    }
}
