using Cko.PaymentGateway.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cko.PaymentGateway.Core.Models
{
    public class ProcessPaymentResponse
    {
        public Guid TransactionReference { get; set; }
        public Guid ExternalReference { get; set; }
        public string CardNumber { get; set; }
        public string Status { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string PaymentType { get; set; }
        public bool IsApproved { get; set; }

        public static ProcessPaymentResponse CreateResponseFromEntity(Transaction transaction)
        {
            var response = new ProcessPaymentResponse
            {
                TransactionReference = transaction.Id,
                ExternalReference = transaction.ExternalReference,
                CardNumber = transaction.CardNumber,
                Status = transaction.Status,
                DateCreated = transaction.DateCreated,
                IsApproved = transaction.Status == "approved"
            };

            return response;
        }
    }
}