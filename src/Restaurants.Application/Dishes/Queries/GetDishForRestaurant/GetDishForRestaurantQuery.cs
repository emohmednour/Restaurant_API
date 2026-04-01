using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Queries.GetDishForRestaurant;

public  class GetDishForRestaurantQuery(int restaurantId, int dishId) : IRequest<DishDto>
{
    public int RestaurantId { get; set; } = restaurantId;
    public int DishId { get; set; } = dishId;
}
