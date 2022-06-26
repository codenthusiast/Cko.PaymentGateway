using System;
using System.Collections.Generic;
using System.Text;

namespace Cko.PaymentGateway.Core.Models
{
    public class CardPaymentRequest : PaymentRequest
    {
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Cvv { get; set; }
    }
}
