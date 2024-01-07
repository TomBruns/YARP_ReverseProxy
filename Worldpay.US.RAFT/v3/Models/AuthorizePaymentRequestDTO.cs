using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Worldpay.US.RAFT.v3.Models
{
    /// <summary>
    /// The information to **Authorize** a Payment request.
    /// </summary>
    [DisplayName("AuthorizePaymentRequest")]
    public class AuthorizePaymentRequestDTO
    {
        /// <summary>
        /// Information about the Merchant submitting this request
        /// </summary>
        /// <value>The merchant data.</value>
        [JsonPropertyName("merchantData")]
        public MerchantDataDTO MerchantData { get; set; }
    }

    /// <summary>
    /// Information about the Merchant submitting this request
    /// </summary>
    [DisplayName("MerchantData")]
    public class MerchantDataDTO
    {
        /// <summary>
        /// The unique merchant account identifier assigned by Worldpay.
        /// </summary>
        /// <value>The merchant identifier.</value>
        [JsonPropertyName("merchantId")]
        [Required]
        public string MerchantId { get; set; }
    }
}
