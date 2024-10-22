using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrnekSite.Entity.Cart
{
    public class Cart//tüm sepeti ifade eder.
    {
        private List<CartLine> _cartLines = new List<CartLine>();
        public List<CartLine> CartLines { 
        
        get { return _cartLines; }

        }
        public void AddProduct(Product product, int quantity)
        {
            var line = _cartLines.FirstOrDefault(i => i.Product.Id == product.Id);
            if (line == null)
            {
                _cartLines.Add(new CartLine()
                {
                    Product = product,
                    Quantity=quantity

                }); 
            }
            else
            {
                line.Quantity += quantity;
            }

        }

        public void DeleteProduct(Product product) {

            var line = _cartLines.FirstOrDefault(x => x.Product.Id == product.Id);
            _cartLines.Remove(line);
        }

        public double Total()
        {
            return _cartLines.Sum(i=>i.Product.Price*i.Quantity);//toplam sepet tutarını hesaplama
        }
        public void Clear()
        {
            _cartLines.Clear();//sepeti temizle.
        }

        
    }
    public class CartLine//bir ürüne ait satırı ifade eder.
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}