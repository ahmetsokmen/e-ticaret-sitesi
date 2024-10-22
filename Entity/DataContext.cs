using OrnekSite.Entity.ShippingDetails;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrnekSite.Entity
{
    public class DataContext:DbContext
    {
        public DataContext():base("dataConnection")
        {
            Database.SetInitializer(new DataInitializer());
        }
        public DbSet<Product> Products { get; set; } //veritabanındaki tablonun ismi products
        public DbSet<Category> Categoris { get; set; }//veritabanındaki tablonun ismi categoris
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderLine> OrderLines { get; set; } //sepeti hiç veri tabanında tutmadık bunun yerine siparişleri ve sipariş satırlarını veritabanında tuttuk.

    }
}