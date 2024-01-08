using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Worldpay.US.JWTTokens
{
    public class IdentityRepoInfoBE
    {
        [JsonPropertyName("tokenSigningInfo")]
        public TokenSigningInfoBE TokenSigningInfo { get; set; }

        [JsonPropertyName("identities")]
        public Dictionary<string, IdentityInfoBE> Identities { get; set; }
    }

    public class TokenSigningInfoBE
    {
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        [JsonPropertyName("audience")]
        public string Audience { get; set; }

        // used for symmetric key signing
        [JsonPropertyName("hmacSecret")]
        public string HMACSecret { get; set; }

        // used for asymmetric key signing
        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set; }

        // used for asymmetric key signing
        [JsonPropertyName("privateKey")]
        public string PrivateKey { get; set; }

        [JsonPropertyName("lifetime")]
        public TimeSpan Lifetime { get; set; }

    }

    public class IdentityInfoBE
    {
        [JsonPropertyName("apiKey")]
        public string ApiKey { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("scopes")]
        public Dictionary<string, Dictionary<string, string>> Scopes { get; set; }
    }
}
