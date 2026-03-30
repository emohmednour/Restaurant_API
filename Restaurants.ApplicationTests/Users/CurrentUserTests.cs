using Xunit;
using Restaurants.Domain.Constant;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class CurrentUserTests
    {
        [Theory]
        [InlineData(UserRoles.Admin)]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {




            //Arrange 
            var user = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);
             
            //act
            var isInRole = user.IsInRole(roleName);


            //assert
            isInRole.Should().BeTrue();




        }

        [Fact()]
        public void IsInRole_WithNOtMatchingRole_ShouldReturnFalse()
        {




            //Arrange 


            var user = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            //act
            var isInRole = user.IsInRole(UserRoles.Owner);


            //assert
            isInRole.Should().BeFalse();




        }

        [Fact()]
        public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
        {




            //Arrange 
            var user = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            //act
            var isInRole = user.IsInRole(UserRoles.Admin.ToLower());


            //assert
            isInRole.Should().BeFalse();




        }
    }
}