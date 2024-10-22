using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrnekSite.Entity.ShippingDetails
{
    public class Order//bütün siparişleri temsil eder.
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserName { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Ilce { get; set; }
        public string Mahalle { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }//bire çok ilişki (çok)
        public OrderState OrderState { get; set; }
    }
    public class OrderLine//tek bir siparişi temsil eder.
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }//bire çok ilişki(bir)
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }//bire bir ilişki orderline-product arasında.



    }
}