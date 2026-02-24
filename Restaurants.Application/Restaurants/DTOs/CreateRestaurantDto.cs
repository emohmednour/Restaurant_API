
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.Restaurants.DTOs;

public  class CreateRestaurantDto
{
    [StringLength(100, MinimumLength = 3, ErrorMessage ="")]
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    [Required(ErrorMessage = "Insert a valid category")]
    public string Category { get; set; } = default!;

    public bool HasDelivery { get; set; }
    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
    public string? ContactEmail { get; set; }
    [Phone(ErrorMessage = "Please provide a valid PhoneNumber")]
    public string? ContactNumber { get; set; }

    public string? City { get; set; }
    public string? Street { get; set; }
    [RegularExpression(@"^\d{2}-\d{3}$",ErrorMessage = "Please provide postal code in format 00-000")]
    public string? PostalCode { get; set; }
}
