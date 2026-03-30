using FluentValidation.TestHelper;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveError()
    {
        //arrange

        var command = new CreateRestaurantCommand() {
            
            Name = "Test rest",
            Category = "Italian",
            ContactEmail = "test@test.com",
            PostalCode = "12-999"
        
        
        };

        var validator = new CreateRestaurantCommandValidator();


        //act
        var result = validator.TestValidate(command);



        //assert

        result.ShouldNotHaveAnyValidationErrors();

    }
    [Fact()]
    public void Validator_ForINValidCommand_ShouldHaveError()
    {
        //arrange

        var command = new CreateRestaurantCommand() {
            
            Name = "Te",
            Category = "Egyptian",
            ContactEmail = "test",
            PostalCode = "12999"
        
        
        };

        var validator = new CreateRestaurantCommandValidator();


        //act
        var result = validator.TestValidate(command);



        //assert

        result.ShouldHaveValidationErrorFor(x=>x.Name);
        result.ShouldHaveValidationErrorFor(x=>x.Category);
        result.ShouldHaveValidationErrorFor(x=>x.ContactEmail);
        result.ShouldHaveValidationErrorFor(x=>x.PostalCode);

    }
    [Theory]
    [InlineData("Italian")]
    [InlineData("Mexican")]
    [InlineData("Japanese")]
    [InlineData("American")]
    [InlineData("Indian")]
    public void Validator_ForValidCategory_ShouldnotHaveError(string category)
    {
        //arrange



        var command = new CreateRestaurantCommand()
        {

          
            Category = category
            


        };

        var validator = new CreateRestaurantCommandValidator();


        //act
        var result = validator.TestValidate(command);



        //assert

        result.ShouldNotHaveValidationErrorFor(x => x.Category);
     

    }




    [Theory]
    [InlineData("12333")]
    [InlineData("ab-ddd")]
    [InlineData("12 366")]
    [InlineData("14-8 5")]
    public void Validator_ForValidPostalCode_ShouldnotHaveError(string PostalCode )
    {
        //arrange



        var command = new CreateRestaurantCommand()
        {


            PostalCode = PostalCode



        };

        var validator = new CreateRestaurantCommandValidator();


        //act
        var result = validator.TestValidate(command);



        //assert

        result.ShouldHaveValidationErrorFor(x => x.PostalCode);


    }
}