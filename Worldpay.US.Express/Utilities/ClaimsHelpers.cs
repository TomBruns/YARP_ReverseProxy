using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
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
    internal static (bool IsWellFormedClaimsObject, ExpressClaimsBE expressClaims) GetExpressClaims(IEnumerable<Claim> claims)
    {
        var expressClaims = new ExpressClaimsBE()
        {
            //Target = claims.FirstOrDefault(c => c.Type == TARGET_CLAIM_NAME)?.Value,
            AcceptorId = claims.FirstOrDefault(c => c.Type == ACCEPTOR_ID_CLAIM_NAME)?.Value,
            AccountToken = claims.FirstOrDefault(c => c.Type == ACCOUNT_TOKEN_CLAIM_NAME)?.Value
        };

        bool IsWellFormedClaimsObject = true == true
                                            && !string.IsNullOrEmpty(expressClaims.AcceptorId)
                                            && !string.IsNullOrEmpty(expressClaims.AccountToken);

        return (IsWellFormedClaimsObject, expressClaims);
    }
}
