
using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Validator;

internal class CreateRestsurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    public CreateRestsurantDtoValidator()
    {
        RuleFor(r => r.Description)
            .NotEmpty()
            .WithMessage("Description is requried.");

        RuleFor(r => r.Category)
            .Must(validCategories.Contains)
            .WithMessage("Invalid category. Choose from valid categories.");


        RuleFor(x => x.ContactEmail)
            .EmailAddress().
            WithMessage("plz provid valid emaill address");


        RuleFor(x => x.PostalCode).
            Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide postal code in format XX-XXX");
    }

    private readonly List<string> validCategories = [
         "Italian",
        "Mexican",
        "Japanese",
        "American",
        "Indian"

        ];






}
