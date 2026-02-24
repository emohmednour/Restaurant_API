using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAllResturants();
    Task<RestaurantDto?> GetRestaurant(int Id);

    Task<int> Create(CreateRestaurantDto dto);
}