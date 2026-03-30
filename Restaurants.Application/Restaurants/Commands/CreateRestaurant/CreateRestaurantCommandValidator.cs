
using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = [
         "Italian",
        "Mexican",
        "Japanese",
        "American",
        "Indian"

        ];
    public CreateRestaurantCommandValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 100);

        RuleFor(r => r.Category)
            .Must(validCategories.Contains)
            .WithMessage("Invalid category. Choose from valid categories.");


        RuleFor(x => x.ContactEmail)
            .EmailAddress().
            WithMessage("plz provide valid email address");


        RuleFor(x => x.PostalCode).
            Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide postal code in format XX-XXX");
    }

    






}
