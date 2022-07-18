using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces
{
    public interface IRepository<T> 
    {
        Task DeleteAsync(T entity, CancellationToken cancellationToken, bool autoCommit = true);
        Task<T> GetAsync(Guid id);
        Task InsertAsync(T entity, CancellationToken cancellationToken, bool autoCommit = true);
        Task UpdateAsync(T entity, CancellationToken cancellationToken, bool autoCommit = true);
    }
}
