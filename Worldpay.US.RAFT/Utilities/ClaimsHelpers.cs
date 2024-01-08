using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Worldpay.US.RAFT.Entities;

namespace Worldpay.US.RAFT.Utilities;

internal static class ClaimsHelpers
{
    internal const string TARGET = @"RAFT";
    internal const string TARGET_CLAIM_NAME = @"target";
    internal const string INTEGRATOR_ID_CLAIM_NAME = @"integratorId";

    /// <summary>
    /// Gets the RAFT claims.
    /// </summary>
    /// <param name="claims">The claims.</param>
    /// <returns>System.ValueTuple&lt;System.Boolean, RAFTClaimsBE&gt;.</returns>
    internal static (bool IsWellFormedClaimsObject, RAFTClaimsBE raftClaims) GetRAFTClaims(IEnumerable<Claim> claims) 
    { 
        var raftClaims = new RAFTClaimsBE()
        {
            Target = claims.FirstOrDefault(c => c.Type == TARGET_CLAIM_NAME)?.Value,
            IntegratorId = claims.FirstOrDefault(c => c.Type == INTEGRATOR_ID_CLAIM_NAME)?.Value
        };

        bool IsWellFormedClaimsObject = raftClaims.Target.ToLower() == TARGET.ToLower()
                                            && !string.IsNullOrEmpty(raftClaims.IntegratorId);

        return (IsWellFormedClaimsObject, raftClaims);
    }
}
