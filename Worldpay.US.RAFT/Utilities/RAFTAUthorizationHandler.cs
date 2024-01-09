using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

using Worldpay.US.RAFT.Entities;

namespace Worldpay.US.RAFT.Utilities;

/// <summary>
/// This class makes sure the RAFT Specific claims are present
/// </summary>
internal class RAFTAuthorizationHandler : AuthorizationHandler<ValidRAFTAuthHeader>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidRAFTAuthHeader requirement)
    {
        (bool isWellFormedClaimsObject, RAFTClaimsBE raftClaims) = ClaimsHelpers.GetRAFTClaims(context.User.Claims);

        if (isWellFormedClaimsObject)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;

        //// Bail if the Scope is not available
        //var claim = context.User.Claims.First(c => c.Type.ToLower() == ClaimsHelpers.SCOPE_CLAIM_NAME.ToLower());
        //if (claim == null || string.IsNullOrEmpty(claim.Value))
        //{
        //    return Task.CompletedTask;
        //}

        //try
        //{
        //    var ourClaims = JsonSerializer.Deserialize<Dictionary<string, string>>(claim.Value);

        //    // Bail if the Integrator Id is not available 
        //    if (!ourClaims.ContainsKey(ClaimsHelpers.INTEGRATOR_ID_CLAIM_NAME) && !string.IsNullOrEmpty(ourClaims[ClaimsHelpers.INTEGRATOR_ID_CLAIM_NAME]))
        //    {
        //        return Task.CompletedTask;
        //    }

        //    // Mark the requirement as satisfied
        //    context.Succeed(requirement);
        //    return Task.CompletedTask;
        //}
        //catch (Exception ex)
        //{
        //    return Task.CompletedTask;
        //}
    }
}

internal class ValidRAFTAuthHeader : IAuthorizationRequirement
{

}
