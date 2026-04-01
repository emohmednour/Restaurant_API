using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests;

public class UpdateRestaurantCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRestaurantsRepository> _restRepoMock;
    private readonly Mock<IRestaurantAuthorizationService> _restAuthSerMock;
    private readonly UpdateRestaurantCommandHandler _handle;

    public UpdateRestaurantCommandHandlerTests()
    {

        _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _restAuthSerMock = new Mock<IRestaurantAuthorizationService>();
        _restRepoMock = new Mock<IRestaurantsRepository>();

        _handle = new UpdateRestaurantCommandHandler(
            _loggerMock.Object,
            _restRepoMock.Object,
            _mapperMock.Object,
            _restAuthSerMock.Object);

    }


    [Fact()]
    public async Task Handle_WithValidRequest_ShouldReturnUpdateRestaurants()
    {

        //arrange
        var restaurantId = 1;

        var command = new UpdateRestaurantCommand
        {
            Id = restaurantId,
            Name = "new update test",
            Description = "des1",
            HasDelivery = false
        };


        var restaurant = new Restaurant
        {
            Id = restaurantId
           
        };

        



        _restRepoMock.Setup(x => x.GetAsync(restaurantId)).
            ReturnsAsync(restaurant);

        _restAuthSerMock.Setup(x => x.Authorize(restaurant, ResourceOperation.Update))
            .Returns(true);

        //act
        await _handle.Handle(command, CancellationToken.None);



        //assert => check
        //map --save
        _mapperMock.Verify(x => x.Map(command,restaurant), Times.Once);
        _restRepoMock.Verify(x => x.SaveChanges(), Times.Once);

    }


    [Fact()]
    public async Task Handle_WithNOtValidRequest_ShouldReturnNotFoundException()
    {

        //arrange
        var restaurantId = 1;

        var command = new UpdateRestaurantCommand
        {
            Id = restaurantId,
            Name = "new update test",
            Description = "des1",
            HasDelivery = false
        };


    

        _restRepoMock.Setup(x => x.GetAsync(restaurantId)).
            ReturnsAsync((Restaurant?)null);

        //act 
        Func<Task> act= async()=> await _handle.Handle(command, CancellationToken.None);

        //assert
        await act.Should().ThrowAsync<NotFoundException>();





    }


    [Fact()]
    public async Task Handle_WithnotValidRequest_ShouldReturnForbidException()
    {

        //arrange
        var restaurantId = 1;

        var command = new UpdateRestaurantCommand
        {
            Id = restaurantId,
            Name = "new update test",
            Description = "des1",
            HasDelivery = false
        };


        var restaurant = new Restaurant
        {
            Id = restaurantId

        };





        _restRepoMock.Setup(x => x.GetAsync(restaurantId)).
            ReturnsAsync(restaurant);

        _restAuthSerMock.Setup(x => x.Authorize(restaurant, ResourceOperation.Update))
            .Returns(false);

        //act
        Func<Task> act = async () => await _handle.Handle(command, CancellationToken.None);



        //assert => check
        await act.Should().ThrowAsync<ForbidException>();

    }
}