using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurantLogo;

public class UpdateRestaurantLogoCommandHandler(ILogger<UpdateRestaurantLogoCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService,
    IBlobStorageService storageService) : IRequestHandler<UpdateRestaurantLogoCommand>
{
    public async Task Handle(UpdateRestaurantLogoCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updateing restaurant Logo by id:{retaurantid}", request.RestaurantId);

        var restaurant = await restaurantsRepository.GetAsync(request.RestaurantId);
        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            throw new ForbidException();
        }


        //upload logo
       var logoUrl =  await storageService.UploadToBlobAsync(request.file, request.FileName);
        restaurant.LogoUrl = logoUrl;

        //save
        await restaurantsRepository.SaveChanges();

    }
}
