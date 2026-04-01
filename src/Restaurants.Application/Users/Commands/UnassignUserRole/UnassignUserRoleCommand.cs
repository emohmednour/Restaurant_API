using MediatR;

namespace Restaurants.Application.Users.Commands.UnassignUserRole;

public  class UnassignUserRoleCommand : IRequest
{
    public string RoleName { get; set; } = default!;
    public string UserEmail { get; set; } = default!;
}
