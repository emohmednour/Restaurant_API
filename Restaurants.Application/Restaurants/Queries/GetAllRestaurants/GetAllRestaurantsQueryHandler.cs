using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger
    ,IMapper mapper,IRestaurantsRepository RestaurantsRepository)
    : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
    public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All restaurants");



        var (restaurants,totalcount) = await RestaurantsRepository.GetAllMatchingAsync(
            request.searchPhrase,
            request.PageSize,
            request.PageNumber);


        var restaurantsDTO = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        return new PagedResult<RestaurantDto>(restaurantsDTO, totalcount, request.PageSize, request.PageNumber);
    }
}
