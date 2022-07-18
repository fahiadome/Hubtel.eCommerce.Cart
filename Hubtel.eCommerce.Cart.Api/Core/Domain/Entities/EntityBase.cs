using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Domain.Entities
{
    public class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime AddedDate { get; set; } 
        public string AddedBy { get; set; }
        public DateTime DateModified { get; set; }
        public string ModifiedBy { get; set; }


        public void WithAddDateAndUser( Guid userId)
        {
            AddedDate = DateTime.Now;
            AddedBy = userId.ToString();
        }
        public void WithModifiedDateAndUser(Guid userId)
        {
            DateModified = DateTime.Now;
            ModifiedBy = userId.ToString();
        }

    }
}
