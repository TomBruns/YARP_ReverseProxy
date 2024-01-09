using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;
using Worldpay.US.RAFT.Entities;

namespace Worldpay.US.RAFT.Utilities;

internal static class ClaimsHelpers
{
    internal const string SCOPE_CLAIM_NAME = @"RAFT";
    internal const string INTEGRATOR_ID_CLAIM_NAME = @"integratorId";

    /// <summary>
    /// Gets the RAFT claims.
    /// </summary>
    /// <param name="claims">The claims.</param>
    /// <returns>System.ValueTuple&lt;System.Boolean, RAFTClaimsBE&gt;.</returns>
    internal static (bool isWellFormedClaimsObject, RAFTClaimsBE raftClaims) GetRAFTClaims(IEnumerable<Claim> claims) 
    {
        var claim = claims.FirstOrDefault(c => c.Type.ToLower() == ClaimsHelpers.SCOPE_CLAIM_NAME.ToLower());
        if (claim == null)
        {
            return (false, null);
        }

        var ourClaims = JsonSerializer.Deserialize<Dictionary<string, string>>(claim.Value);

        var raftClaims = new RAFTClaimsBE()
        {
            IntegratorId = ourClaims.FirstOrDefault(c => c.Key == INTEGRATOR_ID_CLAIM_NAME).Value
        };

        bool isWellFormedClaimsObject = !string.IsNullOrEmpty(raftClaims.IntegratorId);

        return (isWellFormedClaimsObject, raftClaims);
    }
}
