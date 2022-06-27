using System;
using System.Collections.Generic;
using System.Text;

namespace Cko.PaymentGateway.Service.DTOs
{
    public class CardPaymentReponseDTO
    {
        public Guid PaymentReference { get; set; }
        public bool IsApproved { get; set; }
        public string Status { get; set; }
    }
}
