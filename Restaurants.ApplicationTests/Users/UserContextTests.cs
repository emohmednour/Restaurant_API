using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Domain.Constant;
using System.Security.Claims;
using Xunit;


namespace Restaurants.Application.Users.Tests;

public class UserContextTests
{
    [Fact()] 
    public void GetCurrentUser_WithAuthorizationUser_ShouldReturnCurrentUser()
    {

        //Arrange 
        var httpContextAccessorMock = new Mock<IHttpContextAccessor > ();

        var dateOfBirth =new  DateOnly(1999, 1, 1);

        var claims = new List<Claim>() { 
        
           new (ClaimTypes.NameIdentifier ,"1" ),
           new (ClaimTypes.Email ,"test@test.com" ),
           new (ClaimTypes.Role ,UserRoles.Admin ),
           new (ClaimTypes.Role ,UserRoles.User ),
           new ("Nationality" ,"German" ),
           new ("DateOfBirth" , dateOfBirth.ToString("yyyy-MM-dd") )

        
        };

         var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));


        httpContextAccessorMock.Setup(x => x.HttpContext).
            Returns(new DefaultHttpContext() { 
            
                User = user

            });


        //act
        UserContext userContext = new UserContext(httpContextAccessorMock.Object);

       CurrentUser userCurrent = userContext.GetCurrentUser();

        // assert

        userCurrent.Should().NotBeNull();
        userCurrent.Id.Should().Be("1");
        userCurrent.Email.Should().Be("test@test.com");
        userCurrent.Roles.Should().ContainInOrder(UserRoles.Admin,UserRoles.User);
        userCurrent.Nationality.Should().Be("German");
        userCurrent.DateOfBirth.Should().Be(dateOfBirth);







    }

    [Fact()]
    public void GetCurrentUser_WithUserContextNotPresent_ThrowInvalidOperationException(){

        //Arrange 
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext?)null);

        var user = new UserContext(httpContextAccessorMock.Object);



        //act


        Action action = ( ) => user.GetCurrentUser();

        //assert
        action.Should().
            Throw<InvalidOperationException>()
            .WithMessage("user context is not present");

    }
}