using Cko.PaymentGateway.Core.AppExceptions;
using Cko.PaymentGateway.Core.Contracts;
using Cko.PaymentGateway.Core.Models;
using Cko.PaymentGateway.Core.Repository;
using Cko.PaymentGateway.Service.DTOs;
using Cko.PaymentGateway.Service.ThirdParty;
using System;
using System.Threading.Tasks;

namespace Cko.PaymentGateway.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAcquiringBankClient _bankClient;

        public PaymentService(IUnitOfWork unitOfWork, IAcquiringBankClient bankClient)
        {
            _unitOfWork = unitOfWork;
            _bankClient = bankClient;
        }

        public async Task<ProcessPaymentResponse> GetPaymentStatus(Guid transactionId)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(transactionId);

            if(transaction == null)
            {
                throw new PaymentNotFoundException();
            }

            return new ProcessPaymentResponse(transaction);
        }

        public async Task<ProcessPaymentResponse> ProcessCardPayment(CardPaymentRequest request)
        {
            var dto = new CardPaymenRequestDTO
            {
                CardNumber = request.CardNumber,
                Cvv = request.Cvv,
                Amount = request.Amount,
                ExpiryMonth = request.ExpiryMonth,
                ExpiryYear = request.ExpiryYear,
                ExternalReference = request.ExternalReference
            };

            var response = await _bankClient.ProcessCardTransaction(dto);

            var transaction = new Core.Entities.Transaction
            {
                Amount = request.Amount,
                CardEnding = request.CardNumber.Substring(request.CardNumber.Length - 4),
                CardNumber = request.CardNumber.Substring(request.CardNumber.Length - 4).PadLeft(request.CardNumber.Length, '*'),
                Currency = request.Currency,
                CustomerEmail = request.CustomerEmail,
                DateCreated = DateTimeOffset.UtcNow,
                ExternalReference = request.ExternalReference,
                Status = response.Status,
            };

            _unitOfWork.TransactionRepository.Add(transaction);
            await _unitOfWork.SaveAsync();

            return new ProcessPaymentResponse(transaction);
        }
    }
}
