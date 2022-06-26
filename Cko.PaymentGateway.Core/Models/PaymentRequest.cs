using System;

namespace Cko.PaymentGateway.Core.Models
{
    public abstract class PaymentRequest
    {
        public string Amount { get; set; }
        public string Currency { get; set; }
        public Guid ExternalReference { get; set; }
        public Guid MerchantId { get; set; }
        public string CustomerEmail { get; set; }
    }
}