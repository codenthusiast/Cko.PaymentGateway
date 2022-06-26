using Cko.PaymentGateway.Core.Entities;
using Cko.PaymentGateway.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cko.PaymentGateway.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            TransactionRepository = new BaseRepository<Transaction>(_dbContext);
            MerchantRepository = new BaseRepository<Merchant>(_dbContext);
        }
        public IBaseRepository<Transaction> TransactionRepository { get; private set; }
        public IBaseRepository<Merchant> MerchantRepository { get; private set; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public Task SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
