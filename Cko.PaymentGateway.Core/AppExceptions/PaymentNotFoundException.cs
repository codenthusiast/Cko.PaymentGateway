using System;
using System.Collections.Generic;
using System.Text;

namespace Cko.PaymentGateway.Core.AppExceptions
{
    public class PaymentNotFoundException : Exception
    {
        public PaymentNotFoundException() : base()
        {

        }

        public PaymentNotFoundException(string message) : base(message)
        {

        }
    }
}
