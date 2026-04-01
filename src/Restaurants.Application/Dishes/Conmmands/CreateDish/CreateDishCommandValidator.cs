using FluentValidation;

namespace Restaurants.Application.Dishes.Conmmands.CreateDish;

public  class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(d => d.Price).
            GreaterThanOrEqualTo(0).
            WithMessage("The Price must be non-nigative a number");


        RuleFor(d => d.KiloCalories).
            GreaterThanOrEqualTo(0).
            When(d=>d.KiloCalories.HasValue).
            WithMessage("The KiloCalories must be non-nigative a number");
    }
}
