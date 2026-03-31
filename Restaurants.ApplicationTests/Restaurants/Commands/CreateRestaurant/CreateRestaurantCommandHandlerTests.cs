using Xunit;

using Moq;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Entities;
using FluentAssertions;
namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_CreatedRestaurantId()
        {

            //arrange
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

            var mapperMock = new Mock<IMapper>();

            var userContextMock = new Mock<IUserContext>();

            var currentUser = new CurrentUser("Owner_Id","test@test.com",[],null, null);

            userContextMock.Setup(x => x.GetCurrentUser()).
                Returns(currentUser);

            var repositoryRestaurantMock = new Mock<IRestaurantsRepository>();

            repositoryRestaurantMock.Setup(x => x.Create(It.IsAny<Restaurant>())).
                ReturnsAsync(1);

            var restaurant = new Restaurant();
            var command = new CreateRestaurantCommand();

            mapperMock.Setup(x => x.Map<Restaurant>(command))
                .Returns(restaurant);

            var Handler = new CreateRestaurantCommandHandler(
                loggerMock.Object,
                mapperMock.Object,
                repositoryRestaurantMock.Object,
                userContextMock.Object
                );

            //act 
            var result = await Handler.Handle(command,CancellationToken.None);

            // assert
            //is id is correct?
            result.Should().Be(1);


            //is OwnerId == currentUser.Id?
            restaurant.OwnerId.Should().Be(currentUser.Id);

            //is method Create() Called once?
            repositoryRestaurantMock.
                Verify(x => x.Create(restaurant), Times.Once);





        }
    }
}