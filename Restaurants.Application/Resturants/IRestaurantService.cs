using Restaurants.Application.Resturants.DTOs;

namespace Restaurants.Application.Resturants;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAllResturants();
    Task<RestaurantDto?> GetRestaurant(int Id);

    Task<int> Create(CreateRestaurantDto dto);
}