using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization.Requirements;
using System.Threading.Tasks;
using Xunit;

namespace Restaurants.Infrastructure.Tests.Authorization.Requirements;

public class CreatedMultipleRestaurantsRequirementHandlerTests
{
    [Fact()]
    public async Task HandleASync_UserHasEnoughRestaurant_shouldSuccessed()
    {
        //arrange
        var usercurrent = new CurrentUser("1", "test@test.com", [], null, null);
        var ContextUSerMock = new Mock<IUserContext>();

        ContextUSerMock.Setup(x=> x.GetCurrentUser()).Returns(usercurrent);

        var restaurunts = new List<Restaurant>()
        {
            new(){
                OwnerId= usercurrent.Id
            },
            new(){
                OwnerId= usercurrent.Id
            },
            new(){
                OwnerId= "2"
            }


        };

        var restaurantRepoMock = new Mock<IRestaurantsRepository>();

        restaurantRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(restaurunts);

        var handler = new CreatedMultipleRestaurantsRequirementHandler(
            
            ContextUSerMock.Object,
            restaurantRepoMock.Object);

        var requirment =new CreatedMultipleRestaurantsRequirement(2);

        var context = new AuthorizationHandlerContext([requirment], null, null);


        //act
       await handler.HandleAsync(context);


        //assert
        context.HasSucceeded.Should().BeTrue();

    }


    [Fact()]
    public void HandleASync_UserHasNOtEnoughRestaurant_shouldFailed()
    {
        //arrange
        var usercurrent = new CurrentUser("1", "test@test.com", [], null, null);
        var ContextUSerMock = new Mock<IUserContext>();

        ContextUSerMock.Setup(x => x.GetCurrentUser()).Returns(usercurrent);

        var restaurunts = new List<Restaurant>()
        {
            new(){
                OwnerId= usercurrent.Id
            },
            
            new(){
                OwnerId= "2"
            }


        };

        var loggerMock = new Mock<ILogger<CreatedMultipleRestaurantsRequirementHandler>>();
        var restaurantRepoMock = new Mock<IRestaurantsRepository>();

        restaurantRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(restaurunts);

        var handler = new CreatedMultipleRestaurantsRequirementHandler(
           
            ContextUSerMock.Object,
            restaurantRepoMock.Object);

        var requirment = new CreatedMultipleRestaurantsRequirement(2);

        var context = new AuthorizationHandlerContext([requirment], null, null);


        //act
        handler.HandleAsync(context);


        //assert
        context.HasSucceeded.Should().BeFalse(); 
        context.HasFailed.Should().BeTrue();

    }
}