using System;
using System.Collections.Generic;
using System.Text;

namespace Cko.PaymentGateway.Core.Entities
{
    public class Merchant
    {
        public Guid Id { get; set; }
        public int BusinessName { get; set; }
        public DateTime Created { get; set; }
        //public ICollection<Customer> Customers { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
