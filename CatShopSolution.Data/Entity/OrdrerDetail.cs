using System;
using System.Collections.Generic;
using System.Text;

namespace CatShopSolution.Data.Entity
{
    public class OrderDetail
    {
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public Order Order { get; set; }

        public Product Product { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
