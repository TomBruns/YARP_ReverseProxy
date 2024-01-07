using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Worldpay.US.JWTTokens;

public static class JWTUtils
{
    /// <summary>
    /// Creates the hmac (Symmetric algorithm) token.
    /// </summary>
    /// <param name="claims">The claims.</param>
    /// <param name="validDuration">Duration of the valid.</param>
    /// <param name="issuer">The issuer.</param>
    /// <param name="audience">The audience.</param>
    /// <param name="tokenSecret">The token secret.</param>
    /// <returns>System.String.</returns>
    //public static string CreateHMACToken(Dictionary<string, object> claims, TimeSpan validDuration, string issuer, string audience, string tokenSecret)
    //{
    //    // add std claims
    //    claims.Add(JwtRegisteredClaimNames.Iss, issuer);
    //    claims.Add(JwtRegisteredClaimNames.Aud, audience);
    //    claims.Add(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds());
    //    claims.Add(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.Add(validDuration).ToUnixTimeSeconds());

    //    var token = JWT.HS256.Create(claims, tokenSecret);

    //    return token;
    //}

    //public static (bool isValid, Dictionary<string, object> claims) ValidateHMACToken(string token, string tokenSecret)
    //{
    //    // validate the token
    //    if (!JWT.HS256.ValidateSignature(token, tokenSecret))
    //    {
    //        return (false, null);
    //    }

    //    var claims = JWT.Read<Dictionary<string, object>> (token);

    //    return (true, claims);
    //}

    //public static (bool isValid, Dictionary<string, object> claims) ValidateHMACToken2(string token, string tokenSecret)
    //{
    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    try
    //    {
    //        tokenHandler.ValidateToken(token, new TokenValidationParameters
    //        {
    //            ValidateIssuerSigningKey = true,
    //            ValidateIssuer = true,
    //            ValidateAudience = true,
    //            ValidIssuer = myIssuer,
    //            ValidAudience = myAudience,
    //            IssuerSigningKey = mySecurityKey
    //        }, out SecurityToken validatedToken);
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //    return true;

    //    return (true, claims);
    //}

    //public static string CreateRSAToken(Dictionary<string, object> claims, TimeSpan validDuration, string privateKey)
    //{
    //    // add std claims
    //    claims.Add(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds());
    //    claims.Add(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.Add(validDuration).ToUnixTimeSeconds());

    //    var token = JWT.RS256.Create(claims, privateKey);

    //    return token;
    //}
}
