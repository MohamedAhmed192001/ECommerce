using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IPhotoRepository PhotoRepository { get; }
        public ICustomerBasketRepository CustomerBasket { get; }
    }
}
