using Cko.PaymentGateway.Core.Models;
using System.Threading.Tasks;

namespace Cko.PaymentGateway.Core.Contracts
{
    public interface IPaymentService
    {
        Task<ProcessPaymentResponse> ProcessPayment(CardPaymentRequest paymentRequest);
        Task<ProcessPaymentResponse> GetPaymentStatus(string transactionId);
    }
}
