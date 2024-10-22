using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrnekSite.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Stok { get; set; }
        public bool Slider { get; set; }//slidera ekli mi
        public bool IsHome { get; set; }//anasayfada görünecek mi
        public bool IsFeatured { get; set; }//öne çıkanlarda görünecek mi
        public bool IsApproved { get; set; }//ürün onaylı mı
        public int CategoryId { get; set; }//her bir ürünün bir kategorisi var.
        public Category Category { get; set; }//her bir ürünün bir kategorisi var.

    }
}