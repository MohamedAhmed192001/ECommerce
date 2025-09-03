using ECommerce.Core.DTOs.Products;
using ECommerce.Core.Entities.Product;
using ECommerce.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<IEnumerable<ViewProductDto>> GetAllAsync(ProductParams productParams);
        public Task<bool> AddAsync(AddProductDto dto);
        public Task<bool> UpdateAsync(UpdateProductDto dto);
        public Task DeleteAsync(Product product);
    }
}
