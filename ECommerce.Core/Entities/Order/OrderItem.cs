﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities.Order
{
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem()
        {

        }
        public OrderItem(int productItemId, string mainImage, string productName, decimal price, int quntity)
        {
            ProductItemId = productItemId;
            MainImage = mainImage;
            ProductName = productName;
            Price = price;
            Quntity = quntity;
        }

        public int ProductItemId { get; set; }
        public string MainImage { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quntity { get; set; }

    }
}
