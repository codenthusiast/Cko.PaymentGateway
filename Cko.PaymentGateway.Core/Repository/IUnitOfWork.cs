using Cko.PaymentGateway.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cko.PaymentGateway.Core.Repository
{
    public interface IUnitOfWork
    {
        IBaseRepository<Transaction> TransactionRepository { get; }
        IBaseRepository<Merchant> MerchantRepository { get; }
        void Dispose();
        Task SaveAsync();
    }
}
