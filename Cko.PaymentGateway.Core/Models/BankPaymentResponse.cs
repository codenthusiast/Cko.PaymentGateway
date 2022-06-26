using System;
using System.Collections.Generic;
using System.Text;

namespace Cko.PaymentGateway.Core.Models
{
    public class BankPaymentResponse
    {
        public Guid TransactionId { get; set; }
        public string TransactionStatus { get; set; }
    }
}
