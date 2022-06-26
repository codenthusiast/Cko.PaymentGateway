using Cko.PaymentGateway.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cko.PaymentGateway.Core.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task Save(T transaction);
        Task<T> GetById(Guid id);
        Task<T> Update(Guid id, T transaction);
        Task Delete(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(int? pageSize = null, int? page = null);
        Task<IEnumerable<T>> GetByDateRange (DateTime? startDate, DateTime? endDate);  
    }
}
