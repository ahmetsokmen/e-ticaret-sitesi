using OrnekSite.Entity;
using OrnekSite.Entity.ShippingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrnekSite.Entity.ShippingDetails
{
    public class OrderDetails
    {
       
            public int OrderId { get; set; }
            public string OrderNumber { get; set; }
            public double Total { get; set; }
            public DateTime OrderDate { get; set; }
            public OrderState OrderState { get; set; }
            public string UserName { get; set; }
            public string Adres { get; set; }
            public string Sehir { get; set; }
            public string Ilce { get; set; }
            public string Mahalle { get; set; }
            public virtual List<OrderLineModel> OrderLines { get; set; }
    

        }
    public class OrderLineModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

    }
}