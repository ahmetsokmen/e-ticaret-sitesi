using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OrnekSite.Entity
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
            
        {
            var kategoriler = new List<Category>()
            {
                new Category(){Name="Kamera",Description="Kamera Ürünleri"},
                new Category(){Name="Telefon",Description="Telefon Ürünleri"},
                new Category(){Name="Bilgisayar",Description="Bilgisayar Ürünleri"}
            };

            foreach (var item in kategoriler)
            {
                context.Categoris.Add(item);
            }
            context.SaveChanges();
            var urunler = new List<Product>()
            {
                new Product(){Name="Canon",Description="kamera ürünleri",Price=2500,Stok=125,IsHome=true,IsApproved=true,IsFeatured=false,Slider=true, CategoryId=1,Image="1.jpg"},
                 new Product(){Name="asus",Description="bilgisayar ürünleri",Price=2000,Stok=100,IsHome=true,IsApproved=true,IsFeatured=true,Slider=true, CategoryId=3,Image="2.jpg"},
                  new Product(){Name="lenovo",Description="bilgisayar ürünleri",Price=3500,Stok=50,IsHome=false,IsApproved=true,IsFeatured=true,Slider=false, CategoryId=3,Image="3.jpg"},
                  new Product(){Name="Samsung 6s",Description="telefon ürünleri",Price=5000,Stok=150,IsHome=false,IsApproved=true,IsFeatured=true,Slider=true, CategoryId=2,Image="4.jpg"}
            };


            foreach (var item in urunler)
            {
                context.Products.Add(item);
            }

            context.SaveChanges();


            base.Seed(context);
        }
    }
}