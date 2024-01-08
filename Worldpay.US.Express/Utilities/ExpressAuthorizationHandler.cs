using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace Worldpay.US.Express.Utilities;

/// <summary>
/// This class makes sure the Express Specific claims are present
/// </summary>
internal class ExpressAuthorizationHandler : AuthorizationHandler<ValidExpressAuthHeader>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidExpressAuthHeader requirement)
    {
        // Bail if the Target is not available
        var claim = context.User.Claims.First(c => c.Type.ToLower() == ClaimsHelpers.SCOPE_CLAIM_NAME.ToLower());
        if (claim == null || string.IsNullOrEmpty(claim.Value))
        {
            return Task.CompletedTask;
        }

        try
        {
            var ourClaims = JsonSerializer.Deserialize<Dictionary<string, string>>(claim.Value);

            // Bail if the Acceptor Id is not available 
            if (!ourClaims.ContainsKey(ClaimsHelpers.ACCEPTOR_ID_CLAIM_NAME) && !string.IsNullOrEmpty(ourClaims[ClaimsHelpers.ACCEPTOR_ID_CLAIM_NAME]))
            {
                return Task.CompletedTask;
            }

            // Bail if the Account Token is not available 
            if (!ourClaims.ContainsKey(ClaimsHelpers.ACCOUNT_TOKEN_CLAIM_NAME) && !string.IsNullOrEmpty(ourClaims[ClaimsHelpers.ACCOUNT_TOKEN_CLAIM_NAME]))
            {
                return Task.CompletedTask;
            }

            // Mark the requirement as satisfied
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            return Task.CompletedTask;
        }
    }
}

internal class ValidExpressAuthHeader : IAuthorizationRequirement
{

}