using System;
using System.Collections.Generic;
using System.Text;

namespace Cko.PaymentGateway.Service.DTOs
{
    public class CardPaymentDTO
    {
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Cvv { get; set; }
        public decimal Amount { get; set; }
        public Guid ExternalReference { get; set; }
    }
}
