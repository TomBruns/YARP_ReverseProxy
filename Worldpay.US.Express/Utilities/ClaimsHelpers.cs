using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;
using Worldpay.US.Express.Entities;

namespace Worldpay.US.Express.Utilities;

public class ClaimsHelpers
{
    internal const string SCOPE_CLAIM_NAME = @"EXPRESS";
    internal const string ACCEPTOR_ID_CLAIM_NAME = @"acceptorId";
    internal const string ACCOUNT_TOKEN_CLAIM_NAME = @"accountToken";

    /// <summary>
    /// Gets the Express claims.
    /// </summary>
    /// <param name="claims">The claims.</param>
    /// <returns>System.ValueTuple&lt;System.Boolean, ExpressClaimsBE&gt;.</returns>
    internal static (bool isWellFormedClaimsObject, ExpressClaimsBE expressClaims) GetExpressClaims(IEnumerable<Claim> claims)
    {
        var claim = claims.FirstOrDefault(c => c.Type.ToLower() == ClaimsHelpers.SCOPE_CLAIM_NAME.ToLower());
        if (claim == null)
        {
            return (false, null);
        }

        var ourClaims = JsonSerializer.Deserialize<Dictionary<string, string>>(claim.Value);

        var expressClaims = new ExpressClaimsBE()
        {
            AcceptorId = ourClaims.FirstOrDefault(c => c.Key == ACCEPTOR_ID_CLAIM_NAME).Value,
            AccountToken = ourClaims.FirstOrDefault(c => c.Key == ACCOUNT_TOKEN_CLAIM_NAME).Value
        };

        bool isWellFormedClaimsObject = !string.IsNullOrEmpty(expressClaims.AcceptorId)
                                            && !string.IsNullOrEmpty(expressClaims.AccountToken);

        return (isWellFormedClaimsObject, expressClaims);
    }
}
