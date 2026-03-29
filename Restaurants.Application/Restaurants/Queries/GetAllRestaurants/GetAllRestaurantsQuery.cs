using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Constant;
using System.ComponentModel;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery :IRequest<PagedResult<RestaurantDto>>
{
    public string? searchPhrase { get; set; }
    public int PageSize { get; set; } 
    public int PageNumber { get; set; } 


    public string? SortBy { get; set; }
    public SortDirection sortDirection { get; set; }
}
