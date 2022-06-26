using System;

namespace Cko.PaymentGateway.Core.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Merchant Merchant { get; set; }
        public Guid MerchantId { get; set; }
    }
}
