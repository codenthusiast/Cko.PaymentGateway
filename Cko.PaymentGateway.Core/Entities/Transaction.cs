using System;
using System.Collections.Generic;
using System.Text;

namespace Cko.PaymentGateway.Core.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string CustomerEmail { get; set; }
        public Guid ExternalReference { get; set; }
        public string CardEnding { get; set; }
        public string Status { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public Guid MerchantId { get; set; }
        public Merchant Merchant { get; set; }
    }
}
