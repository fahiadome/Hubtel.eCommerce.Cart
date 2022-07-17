using System.Collections.Generic;
using System.Threading.Tasks;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories;
using Hubtel.eCommerce.Cart.Domain.Entities;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<User> GetAll();
        User GetById(string id);
        Task Login();
    }
}
