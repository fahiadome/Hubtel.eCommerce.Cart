﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces
{
    public interface IRepository<T> 
    {
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        //Task SoftDeleteAsync(T entity, bool autoCommit = true);
        Task CommitAsync();
        Task CommitAsync(CancellationToken cancellationToken);
        Task DeleteAsync(T entity, CancellationToken cancellationToken, bool autoCommit = true);
        Task DeleteAsync(T entity, bool autoCommit = true);
        Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool autoCommit = true);
        Task DeleteAsync(IEnumerable<T> entities, bool autoCommit = true);
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Guid id);
       Task<List<T>> GetAllAsync();
        //Task<PaginatedList<T>> GetPage(PaginatedCommand paginated, CancellationToken cancellationToken);
        //Task<PaginatedList<T>> GetPage(PaginatedCommand paginated, IQueryable<T> query, CancellationToken cancellationToken);
        Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool autoCommit = true);
        Task InsertAsync(IEnumerable<T> entities, bool autoCommit = true);
        Task InsertAsync(T entity, bool autoCommit = true);
        Task InsertAsync(T entity, CancellationToken cancellationToken, bool autoCommit = true);
        Task UpdateAsync(T entity, CancellationToken cancellationToken, bool autoCommit = true);
        Task UpdateAsync(T entity, bool autoCommit = true);
        IQueryable<T> GetBaseQuery();
    }
}
