using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Security.Claims;

namespace Restaurants.API
{
    public class FakePolicyEvaluator : IPolicyEvaluator
    {
        public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            var claimPrincipal = new ClaimsPrincipal();

            claimPrincipal.AddIdentity( new ClaimsIdentity(new[] {
            
               new Claim(ClaimTypes.NameIdentifier , "1"),
                new Claim(ClaimTypes.Role , "Admin"),
            }));

            var ticket = new AuthenticationTicket(claimPrincipal,"FakeScheme");

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);

        }

        public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
        {
           return Task.FromResult(PolicyAuthorizationResult.Success());
        }
    }
}
