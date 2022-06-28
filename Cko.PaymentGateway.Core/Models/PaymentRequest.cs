using System;
using System.ComponentModel.DataAnnotations;

namespace Cko.PaymentGateway.Core.Models
{
    public abstract class PaymentRequest
    {
        [Range(minimum:1.0, maximum: 10000000, ErrorMessage = "Invalid amount")]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(3, ErrorMessage = "Invalid currency")]
        public string Currency { get; set; }
        [Required]
        public Guid ExternalReference { get; set; }
        public Guid MerchantId { get; set; }
        public string CustomerEmail { get; set; }
    }
}