using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;
using FluentAssertions;
namespace Restaurants.API.Middlewares.Tests;

public class ErrorHandlingMiddlewareTests
{
    [Fact()]
    public async Task InvokeAsync_WithException_ShouldCallNextDelegate()
    {
        //arrange

        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var logger = loggerMock.Object;
        var middleware = new ErrorHandlingMiddleware(logger);
        var context = new DefaultHttpContext();

        var nextdelegetMock = new Mock<RequestDelegate>();
        //act

        await middleware.InvokeAsync(context,nextdelegetMock.Object);

        // assert

        nextdelegetMock.Verify( next =>next.Invoke(context) , Times.Once);


    }


    [Fact()]
    public async Task InvokeAsync_WithNotFoundException_ShouldReturn404()
    {
        //arrange
        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var logger = loggerMock.Object;
        var middleware = new ErrorHandlingMiddleware(logger);
        var context = new DefaultHttpContext();
        
        RequestDelegate next = cfx=> throw new NotFoundException(nameof(Restaurant),"1");
        
        //act
        await middleware.InvokeAsync(context, next);

        //assert
        context.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);


    }


    [Fact()]
    public async Task InvokeAsync_WithForbiddenException_ShouldReturn403()
    {
        // arrange 

        var logger = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middleware = new ErrorHandlingMiddleware(logger.Object);

        var context = new DefaultHttpContext();

        RequestDelegate next = _ => throw new ForbidException();

        // act
        await middleware.InvokeAsync(context, next);

        //assert 

        context.Response.StatusCode.Should().Be(StatusCodes.Status403Forbidden);


    }
    [Fact()]
    public async Task InvokeAsync_WithGenericException_ShouldReturn500()
    {
        // arrange 

        var logger = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middleware = new ErrorHandlingMiddleware(logger.Object);

        var context = new DefaultHttpContext();

        RequestDelegate next = _ => throw new Exception();

        // act
        await middleware.InvokeAsync(context, next);

        //assert 

        context.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);


    }
}