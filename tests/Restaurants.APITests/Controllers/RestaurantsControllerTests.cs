using Restaurants.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization.Policy;
using Restaurants.Domain.Repositories;
using Moq;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Application.Restaurants.DTOs;
using System.Net.Http.Json;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.API.Controllers.Tests;

public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepository = new();
    private readonly Mock<IRestaurantSeeder> _restaurantSeeder = new();
    public RestaurantsControllerTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory.WithWebHostBuilder(builder =>
        {

            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                // كده كل ريكوست جاي هنتعامل معاه كانه  (Authenticated + Authorized )

                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository)
                    , _ => _restaurantsRepository.Object));
                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantSeeder)
                    , _ => _restaurantSeeder.Object));

            });

        });
    }

    [Fact()]
    public async Task GetAll_WithValid_Return200Ok()
    {
        //arrange 
        var client = _webApplicationFactory.CreateClient();

        //act
        var result = await client.GetAsync("/api/restaurants?PageSize=5&PageNumber=1");

        //assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);

    }


    [Fact()]
    public async Task GetAll_WithInValidRequest_Return400BadRequest()
    {
        //arrange 
        var client = _webApplicationFactory.CreateClient();

        //act
        var result = await client.GetAsync("/api/restaurants");

        //assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    }

    [Fact()]
    public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
    {

        var id = 489;

        //arrange 
        var client = _webApplicationFactory.CreateClient();
        _restaurantsRepository.Setup(x => x.GetAsync(id)).ReturnsAsync((Restaurant?)null);

        //act
        var result = await client.GetAsync($"/api/restaurants/{id}");

        //assert
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Fact()]
    public async Task GetById_ForExistingId_ShouldReturn200Ok()
    {// arrange

        var id = 99;

        var restaurant = new Restaurant()
        {
            Id = id,
            Name = "Test",
            Description = "Test description"
        };

        _restaurantsRepository.Setup(m => m.GetAsync(id)).ReturnsAsync(restaurant);

        var client = _webApplicationFactory.CreateClient();

        // act
        var response = await client.GetAsync($"/api/restaurants/{id}");
        var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        restaurantDto.Should().NotBeNull();
        restaurantDto.Name.Should().Be("Test");
        restaurantDto.Description.Should().Be("Test description");
    }



}