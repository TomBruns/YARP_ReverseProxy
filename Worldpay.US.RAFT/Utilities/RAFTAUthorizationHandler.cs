using Microsoft.AspNetCore.Authorization;

namespace Worldpay.US.RAFT.Utilities
{
    internal class RAFTAuthorizationHandler : AuthorizationHandler<ValidRAFTAuthHeader>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidRAFTAuthHeader requirement)
        {
            // Bail if the Target is not available or not the expected value
            if (!context.User.HasClaim(c => c.Type == ClaimsHelpers.TARGET_CLAIM_NAME && c.Value == ClaimsHelpers.TARGET))
            {
                return Task.CompletedTask;
            }

            // Bail if the Integrator Id is not available 
            if (!context.User.HasClaim(c => c.Type == ClaimsHelpers.INTEGRATOR_ID_CLAIM_NAME && !string.IsNullOrEmpty(c.Value)))
            {
                return Task.CompletedTask;
            }

            // Mark the requirement as satisfied
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    internal class ValidRAFTAuthHeader : IAuthorizationRequirement
    {

    }
}
