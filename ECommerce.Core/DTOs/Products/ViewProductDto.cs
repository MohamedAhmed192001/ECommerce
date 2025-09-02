using ECommerce.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.DTOs.Products
{
    public class ViewProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        
        public string CategoryName { get; set; } = string.Empty;

        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}
