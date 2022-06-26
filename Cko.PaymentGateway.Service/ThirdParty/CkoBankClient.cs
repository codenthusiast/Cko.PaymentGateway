using Cko.PaymentGateway.Core.Models;
using Cko.PaymentGateway.Service.DTOs;
using System;
using System.Threading.Tasks;

namespace Cko.PaymentGateway.Service.ThirdParty
{
    public class CkoBankClient : IAcquiringBankClient
    {
        public Task<CardPaymentReponseDTO> ProcessCardTransaction(CardPaymentDTO request)
        {
            throw new NotImplementedException();
        }
    }
}