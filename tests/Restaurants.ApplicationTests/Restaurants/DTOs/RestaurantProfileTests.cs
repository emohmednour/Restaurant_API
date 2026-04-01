using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;
using Xunit;


namespace Restaurants.Application.Restaurants.DTOs.Tests;

public class RestaurantProfileTests
{
    private IMapper _mapper;

    public RestaurantProfileTests()
    {
        var confg = new MapperConfiguration(confg =>
        {
            confg.AddProfile<RestaurantProfile>();
        });

        _mapper = confg.CreateMapper();
    
    }


    [Fact()]
    public void Map_Restaurant_To_RestaurantDTo_ShouldMapReturnCorrectery()
    {

        //arrange
        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test",
            Category = "Italian",
            ContactEmail = "test@test.com",
            Description = "des",
            HasDelivery = true,
            ContactNumber = "4545577",
            Address = new Address
            {
                City = "Egypt",
                PostalCode = "14-555",
                Street= "Maser"
            }
        };

        //act

        var dto = _mapper.Map<RestaurantDto>(restaurant);

        //assert

        dto.Should().NotBeNull();
        dto.Name.Should().Be(restaurant.Name);
        dto.Id.Should().Be(restaurant.Id);
        dto.Description.Should().Be(restaurant.Description);
        dto.Category.Should().Be(restaurant.Category);
        dto.HasDelivery.Should().Be(restaurant.HasDelivery);
       

        

        dto.City.Should().Be(restaurant.Address.City);
        dto.PostalCode.Should().Be(restaurant.Address.PostalCode);
        dto.Street.Should().Be(restaurant.Address.Street);
        

    }







    [Fact()]
    public void Map_CreateRestaurantCommand_To_Restaurant_ShouldMapReturnCorrector()
    {

        //arrange
        var command = new CreateRestaurantCommand
        {
           
            Name = "Test created",
            Category = "Indian",
            ContactEmail = "testcreated@test.com",
            Description = "dews",
            HasDelivery = false,
            ContactNumber = "4545577",

                City = "Egypt2",
                PostalCode = "12-555",
                Street = "Maser2"
            
        };

        //act

        var restaurant = _mapper.Map<Restaurant>(command);

        //assert

        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.Category.Should().Be(command.Category);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);
        restaurant.ContactEmail.Should().Be(command.ContactEmail);
        restaurant.ContactEmail.Should().Be(command.ContactEmail);





        restaurant.Address!.City.Should().Be(command.City);
        restaurant.Address.PostalCode.Should().Be(command.PostalCode);
        restaurant.Address.Street.Should().Be(command.Street);


    }





    [Fact()]
    public void Map_UpdateRestaurantCommand_To_Restaurant_ShouldMapReturnCorrector()
    {

        //arrange
        var command = new UpdateRestaurantCommand
        {
            Id= 1,
            Name = "Test  Update",
            Description = " Update",
            HasDelivery = false,

        
        };

        //act

        var restaurant = _mapper.Map<Restaurant>(command);

        //assert

        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);





    }







}