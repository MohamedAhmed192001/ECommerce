using ECommerce.Core.Entities.Product;
using System;
using System.Collections.Generic;
namespace ECommerce.Core.DTOs.Products
{
    public class ViewProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}
