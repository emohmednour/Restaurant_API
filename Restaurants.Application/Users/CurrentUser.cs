
namespace Restaurants.Application.Users;

public record CurrentUser(string Id,string Email ,IEnumerable<string> Roles,
    string? Nationnality,DateOnly? DateOfBirth)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
