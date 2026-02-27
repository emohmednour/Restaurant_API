using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Queries.GetRestaurant;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Exceptions;
namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
    IMapper mapper,
    IRestaurantRepository restaurantRepository)
    : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
   

    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation( "Getting restaurant {RestaurantId}" ,request.Id);

        var restaurant = await restaurantRepository.GetAsync(request.Id) ??
            throw new NotFoundException($"Something errors with {request.Id}");

        var restaurantDTO = mapper.Map<RestaurantDto>(restaurant);
            

        return restaurantDTO;
    }
}
