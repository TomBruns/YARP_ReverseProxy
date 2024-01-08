using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Worldpay.US.Express.v4.Models;

/// <summary>
/// Class AuthorizePaymentResponseDTO.
/// </summary>
[DisplayName("AuthorizePaymentResponse")]
public class AuthorizePaymentResponseDTO
{
    /// <summary>
    /// Gets or sets the authorize result.
    /// </summary>
    /// <value>The authorize result.</value>
    [JsonPropertyName("authorizeResult")]
    public string AuthorizeResult { get; set; }
}
