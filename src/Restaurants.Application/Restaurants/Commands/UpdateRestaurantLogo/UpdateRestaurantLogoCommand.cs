

using MediatR;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurantLogo;
public class UpdateRestaurantLogoCommand : IRequest
{

    public int RestaurantId { get; set; }
    public string FileName { get; set; } = default!;
    public Stream file { get; set; } = default!;
}

