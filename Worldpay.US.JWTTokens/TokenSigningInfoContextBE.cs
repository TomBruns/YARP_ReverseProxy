using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Worldpay.US.JWTTokens
{
    /// <summary>
    /// Suppport a JSON Serializer via Source Generation
    /// </summary>
    /// <remarks>
    /// https://learn.microsoft.com/en-us/dotnet/core/compatibility/serialization/6.0/timespan-serialization-format
    /// </remarks>
    [JsonSerializable(typeof(TokenSigningInfoBE))]
    public partial class TokenSigningInfoContextBE : JsonSerializerContext
    {
    }
}
