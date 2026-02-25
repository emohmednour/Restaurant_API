using MediatR;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurant;

public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDto?>
{
    public int Id { get; } = id;
}
