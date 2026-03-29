

using FluentValidation;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{

    private int[] allowedPagedSize = [5,10,15,30];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);
            //.InclusiveBetween(1, 50)
        

        RuleFor(r => r.PageSize)
            .Must(value => allowedPagedSize.Contains(value)).
            WithMessage($"Page size must be in [{string.Join(",",allowedPagedSize)}]");
     }

}

