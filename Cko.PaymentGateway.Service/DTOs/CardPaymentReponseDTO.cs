using System;
using System.Collections.Generic;
using System.Text;

namespace Cko.PaymentGateway.Service.DTOs
{
    public class CardPaymentReponseDTO
    {
        public Guid TransactionReference { get; set; }
        public string Status { get; set; }
    }
}
