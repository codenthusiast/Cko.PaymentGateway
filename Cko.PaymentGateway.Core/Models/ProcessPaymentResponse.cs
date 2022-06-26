using System;
using System.Collections.Generic;
using System.Text;

namespace Cko.PaymentGateway.Core.Models
{
    public class ProcessPaymentResponse
    {
        public Guid TransactionReference { get; set; }
        public Guid ExternalReference { get; set; }
        public string CardEnding { get; set; }
        public string TransactionStatus { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string PaymentType { get; set; }
    }
}