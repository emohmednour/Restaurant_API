

using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{

    private int[] allowedPagedSize = [5, 10, 15, 30];
    private  string[] allowedSortedByColumns = [
        
        nameof(RestaurantDto.Name),
        nameof(RestaurantDto.Description),
        nameof(RestaurantDto.Category)
        
        ];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);
            //.InclusiveBetween(1, 50)
        

        RuleFor(r => r.PageSize)
            .Must(value => allowedPagedSize.Contains(value)).
            WithMessage($"Page size must be in [{string.Join(",",allowedPagedSize)}]");

        RuleFor(r => r.SortBy)
            .Must(value => allowedSortedByColumns.Contains(value)).
            When(e=>e.SortBy != null).
            WithMessage($"sort  must be in [{string.Join(",", allowedSortedByColumns)}]");
     }

}

