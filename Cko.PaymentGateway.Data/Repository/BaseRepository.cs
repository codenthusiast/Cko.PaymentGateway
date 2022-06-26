using Cko.PaymentGateway.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cko.PaymentGateway.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public IAsyncEnumerable<T> GetAllAsync()
        {
            return _dbContext.Set<T>().AsAsyncEnumerable();
        }

        public IAsyncEnumerable<T> GetAllAsync(int? pageSize = null, int? page = null)
        {
            var query = _dbContext.Set<T>()
                        .Skip(page ?? 1)
                        .Take(pageSize ?? 20)
                        .AsNoTracking();
            return query.AsAsyncEnumerable();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            var entity =  _dbContext.Set<T>().FindAsync(id);
            return entity.AsTask();
        }

        public void Add(T transaction)
        {
            _dbContext.Add(transaction);
        }

        public void Update(Guid id, T transaction)
        {
            _dbContext.Update(transaction);
        }
    }
}
