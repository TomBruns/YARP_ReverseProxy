using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.IdentityModel.Tokens;

using Worldpay.US.JWTTokens;

namespace Worldpay.US.IDP;

/// <summary>
/// This is a stand in for a real identity service
/// </summary>
public class IdentityService
{
    private readonly IdentityRepoInfoBE _identityRepoInfo;

    public IdentityService(IdentityRepoInfoBE jwtSigningInfo)
    {
        _identityRepoInfo = jwtSigningInfo;
    }

    /// <summary>
    /// Validates the API key by checking that it is a valid value on file
    /// </summary>
    /// <param name="apiKey">The API key.</param>
    /// <returns>IdentityInfoBE.</returns>
    public IdentityInfoBE ValidateAPIKey(string apiKey)
    {
        if (!_identityRepoInfo.Identities.TryGetValue(apiKey, out var identity))
        {
            return null;
        }

        return identity;
    }

    #region === HMAC (Symmetric) Key Support ===

    // https://dotnetcoretutorials.com/creating-and-validating-jwt-tokens-in-asp-net-core/?expand_article=1

    /// <summary>
    /// Creates the hmac signed jwt.
    /// </summary>
    /// <param name="merchantId">The merchant identifier.</param>
    /// <param name="lifetime">The lifetime.</param>
    /// <returns>System.ValueTuple&lt;System.Boolean, System.String&gt;.</returns>
    public (bool, string) CreateHMACJWT(IdentityInfoBE identity)
    {
        if (identity == null || identity.AddlInfo == null) 
        {
            return (false,  string.Empty);
        }

        // build the custom claims list
        var claims = new Dictionary<string, object>();
        foreach (var kvp in identity.AddlInfo)
        {
            claims.Add(kvp.Key, kvp.Value);
        }

        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_identityRepoInfo.TokenSigningInfo.HMACSecret));
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            //Subject = new ClaimsIdentity(new Claim[]
            //{
            //    new Claim("ABC", "123"),
            //    new Claim("DEF", "456")
            //}),
            Expires = DateTime.UtcNow.Add(_identityRepoInfo.TokenSigningInfo.Lifetime),
            Issuer = _identityRepoInfo.TokenSigningInfo.Issuer,
            Audience = _identityRepoInfo.TokenSigningInfo.Audience,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
            Claims = claims
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return (true, tokenHandler.WriteToken(token));
    }

    //public (bool, string) CreateHMACJWT2(IdentityInfoBE identity)
    //{
    //    if (identity == null || identity.AddlInfo == null)
    //    {
    //        return (false, string.Empty);
    //    }

    //    // build the custom claims list
    //    var claims = new Dictionary<string, object>();
    //    foreach (var kvp in identity.AddlInfo)
    //    {
    //        claims.Add(kvp.Key, kvp.Value);
    //    }

    //    claims.Add(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds());
    //    claims.Add(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.Add(_identityRepoInfo.TokenSigningInfo.Lifetime).ToUnixTimeSeconds());
    //    claims.Add(JwtRegisteredClaimNames.Iss, _identityRepoInfo.TokenSigningInfo.Issuer);
    //    claims.Add(JwtRegisteredClaimNames.Aud, _identityRepoInfo.TokenSigningInfo.Audience);

    //    var token = JWT.HS256.Create(claims, _identityRepoInfo.TokenSigningInfo.HMACSecret);

    //    return (true, token);
    //}

    /// <summary>
    /// Validates the hmacjwt.
    /// </summary>
    /// <param name="hmacToken">The hmac token.</param>
    /// <returns>System.ValueTuple&lt;System.Boolean, Dictionary&lt;System.String, System.String&gt;&gt;.</returns>
    public (bool, Dictionary<string, string>) ValidateHMACJWT(string hmacToken)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_identityRepoInfo.TokenSigningInfo.HMACSecret));

        SecurityToken validatedToken = null;
        ClaimsPrincipal claimsPrincipal = null;

        var tokenHandler = new JwtSecurityTokenHandler();

        // try and validate the token
        try
        {
            claimsPrincipal = tokenHandler.ValidateToken(hmacToken, new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _identityRepoInfo.TokenSigningInfo.Issuer,
                ValidAudience = _identityRepoInfo.TokenSigningInfo.Audience,
                IssuerSigningKey = securityKey
            }, out validatedToken);
        }
        catch
        {
            return (false, null);
        }

        // extract the claims
        var claims = claimsPrincipal.Claims.ToDictionary(x => x.Type, x => x.Value);

        return (true, claims);
    }

    #endregion

    #region === RSA (Asymmetric) Key Support ===

    /// <summary>
    /// Creates the rsajwt.
    /// </summary>
    /// <param name="identity">The identity.</param>
    /// <returns>System.ValueTuple&lt;System.Boolean, System.String&gt;.</returns>
    public (bool, string) CreateRSAJWT(IdentityInfoBE identity)
    {
        if (identity == null || identity.AddlInfo == null)
        {
            return (false, string.Empty);
        }

        // build the custom claims list
        var claims = new Dictionary<string, object>();
        foreach (var kvp in identity.AddlInfo)
        {
            claims.Add(kvp.Key, kvp.Value);
        }

        RSA rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(Convert.FromBase64String(_identityRepoInfo.TokenSigningInfo.PrivateKey), out int bytesRead);

        RsaSecurityKey securityKey = new RsaSecurityKey(rsa);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            //Subject = new ClaimsIdentity(new Claim[]
            //{
            //    new Claim("ABC", "123"),
            //    new Claim("DEF", "456")
            //}),
            Expires = DateTime.UtcNow.Add(_identityRepoInfo.TokenSigningInfo.Lifetime),
            Issuer = _identityRepoInfo.TokenSigningInfo.Issuer,
            Audience = _identityRepoInfo.TokenSigningInfo.Audience,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256),
            Claims = claims
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return (true, tokenHandler.WriteToken(token));
    }

    //public (bool, string) CreateRSAJWT2(IdentityInfoBE identity)
    //{
    //    if (identity == null || identity.AddlInfo == null)
    //    {
    //        return (false, string.Empty);
    //    }

    //    // build the custom claims list
    //    var claims = new Dictionary<string, object>();
    //    foreach (var kvp in identity.AddlInfo)
    //    {
    //        claims.Add(kvp.Key, kvp.Value);
    //    }

    //    claims.Add(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds());
    //    claims.Add(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.Add(_identityRepoInfo.TokenSigningInfo.Lifetime).ToUnixTimeSeconds());
    //    claims.Add(JwtRegisteredClaimNames.Iss, _identityRepoInfo.TokenSigningInfo.Issuer);
    //    claims.Add(JwtRegisteredClaimNames.Aud, _identityRepoInfo.TokenSigningInfo.Audience);

    //    var token = JWT.RS256.Create(claims, _identityRepoInfo.TokenSigningInfo.PrivateKey);

    //    var foo = JWT.RS256.ValidateSignature(token, _identityRepoInfo.TokenSigningInfo.PublicKey);

    //    return (true, token);
    //}

    /// <summary>
    /// Validates the rsajwt.
    /// </summary>
    /// <param name="rsaToken">The RSA token.</param>
    /// <returns>System.ValueTuple&lt;System.Boolean, Dictionary&lt;System.String, System.String&gt;&gt;.</returns>
    public (bool, Dictionary<string, string>) ValidateRSAJWT(string rsaToken)
    {
        RSA rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(_identityRepoInfo.TokenSigningInfo.PublicKey), out int bytesRead);

        RsaSecurityKey securityKey = new RsaSecurityKey(rsa);

        SecurityToken validatedToken = null;
        ClaimsPrincipal claimsPrincipal = null;

        var tokenHandler = new JwtSecurityTokenHandler();

        // try and validate the token
        try
        {
            claimsPrincipal = tokenHandler.ValidateToken(rsaToken, new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _identityRepoInfo.TokenSigningInfo.Issuer,
                ValidAudience = _identityRepoInfo.TokenSigningInfo.Audience,
                IssuerSigningKey = securityKey
            }, out validatedToken);
        }
        catch
        {
            return (false, null);
        }

        // extract the claims
        var claims = claimsPrincipal.Claims.ToDictionary(x => x.Type, x => x.Value);

        return (true, claims);
    }

    #endregion

}
