using Cko.PaymentGateway.Core.Models;
using System;
using System.Threading.Tasks;

namespace Cko.PaymentGateway.Core.Contracts
{
    public interface IPaymentService
    {
        Task<ProcessPaymentResponse> ProcessCardPayment(CardPaymentRequest paymentRequest);
        Task<ProcessPaymentResponse> GetPaymentStatus(Guid transactionId);
    }
}
