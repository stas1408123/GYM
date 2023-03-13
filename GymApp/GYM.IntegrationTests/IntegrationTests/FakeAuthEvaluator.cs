using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GYM.API.IntegrationTests.IntegrationTests
{
    public class FakeAuthEvaluator : IPolicyEvaluator
    {
        {
            public async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
            {
                var testScheme = "FakeScheme";
                var principal = new ClaimsPrincipal();
                principal.AddIdentity(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "FakeName")
                }, testScheme));
                return await Task.FromResult(
                    AuthenticateResult.Success(new AuthenticationTicket(principal, new AuthenticationProperties(),
                        testScheme)));
            }

            public async Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy,
                AuthenticateResult authenticationResult, HttpContext context,
                object? resource)
            {
                return await Task.FromResult(PolicyAuthorizationResult.Success());
            }
        }
    }
}
