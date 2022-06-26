﻿using System;
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
        public string TransactionStatus { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string CardNumber { get; set; }
        public string Expiry { get; set; }
        public string Cvv { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public Guid MerchantId { get; set; }
        public Merchant Merchant { get; set; }
    }
}