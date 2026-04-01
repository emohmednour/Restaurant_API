
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using Xunit;

namespace Restaurants.API.Controllers.Tests;

public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public RestaurantsControllerTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Fact()]
    public async Task GetAll_WithValid_Return200Ok()
    {
        //arrange 
        var client  = _webApplicationFactory.CreateClient();

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
}