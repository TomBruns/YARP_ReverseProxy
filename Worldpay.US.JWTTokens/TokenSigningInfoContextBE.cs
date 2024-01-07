using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Worldpay.US.JWTTokens
{
    [JsonSerializable(typeof(TokenSigningInfoBE))]
    public partial class TokenSigningInfoContextBE : JsonSerializerContext
    {
    }
}
