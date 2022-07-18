using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories
{

	public class Repository<T> : IRepository<T> where T :class
    {
        private readonly ECommerceDbContext _context;
        private DbSet<T> _entities;

		public Repository(ECommerceDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Table { get; }
        public IQueryable<T> TableNoTracking { get; }

		public virtual DbSet<T> Entities
		{
			get
			{
				if (_entities == null)
					_entities = _context.Set<T>();
				return _entities;
			}
		}

		public virtual IQueryable<T> GetBaseQuery()
		{
			return Entities;
		}

        public async Task<T> GetAsync(Guid id)
        {
            var keyProperty = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties[0];
            return await GetBaseQuery().FirstOrDefaultAsync(e => EF.Property<Guid>(e, keyProperty.Name) == id);
        }


		public async Task InsertAsync(T entity, CancellationToken cancellationToken, bool autoCommit = true)
		{
			try
			{
				if (entity == null)
					throw new ArgumentNullException("entity");

				await Entities.AddAsync(entity, cancellationToken);
				if (autoCommit)
					await _context.SaveChangesAsync(cancellationToken);

			}
			catch (DbEntityValidationException dbEx)
			{
				var msg = string.Empty;

				foreach (var validationErrors in dbEx.EntityValidationErrors)
					foreach (var validationError in validationErrors.ValidationErrors)
						msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

				var fail = new Exception(msg, dbEx);
				//Debug.WriteLine(fail.Message, fail);
				throw fail;
			}
			catch (Exception ex) { throw ex; }
		}

		public async Task UpdateAsync(T entity, CancellationToken cancellationToken, bool autoCommit = true)
		{
			try
			{
				if (entity == null)
					throw new ArgumentNullException("entity");

				Entities.Update(entity);
				if (autoCommit)
					await _context.SaveChangesAsync(cancellationToken);
			}
			catch (DbEntityValidationException dbEx)
			{
				var msg = string.Empty;

				foreach (var validationErrors in dbEx.EntityValidationErrors)
					foreach (var validationError in validationErrors.ValidationErrors)
						msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

				var fail = new Exception(msg, dbEx);
				//Debug.WriteLine(fail.Message, fail);
				throw fail;
			}
			catch (Exception ex) { throw ex;}
		}

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken, bool autoCommit = true)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Remove(entity);
                if (autoCommit)
                    await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
