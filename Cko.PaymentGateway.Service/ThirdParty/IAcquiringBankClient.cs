using Cko.PaymentGateway.Core.Models;
using Cko.PaymentGateway.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cko.PaymentGateway.Service.ThirdParty
{
    public interface IAcquiringBankClient
    {
        public Task<CardPaymentReponseDTO> ProcessCardTransaction(CardPaymenRequestDTO request);
    }
}
