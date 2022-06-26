using Cko.PaymentGateway.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cko.PaymentGateway.Core.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T transaction);
        IAsyncEnumerable<T> GetAllAsync();
        IAsyncEnumerable<T> GetAllAsync(int? pageSize = null, int? page = null);
        Task<T?> GetByIdAsync(Guid id);
        void Remove(T entity);
        void Update(Guid id, T transaction);
    }
}
