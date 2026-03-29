using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery :IRequest<PagedResult<RestaurantDto>>
{
    public string? searchPhrase { get; set; }
    public int PageSize { get; set; } = 1;
    public int PageNumber { get; set; } = 10;
}
