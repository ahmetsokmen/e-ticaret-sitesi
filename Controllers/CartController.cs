using OrnekSite.Entity;
using OrnekSite.Entity.Cart;
using OrnekSite.Entity.ShippingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrnekSite.Controllers
{
    public class CartController : Controller
    {
        DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());//getcarddan dönen cartı viewe aktar.
        }
        public Cart GetCart()//kullanıcı sepete ürüne ilk kez ürün ekleyecekse yeni kart verilir. sepette ürün varsa tekrar başka ürün eklemek istediğinde mevcut kartı verir yeni kart vermez.
        {
            var cart = (Cart)Session["Cart"];//cart null değilse mevcut cartı ver.
            if (cart == null)//nullsa yeni cart oluştur onu se
            {
                cart = new Cart();
                Session["Cart"] = cart;//sessiona ekleme.

            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }
            return RedirectToAction("Index");





        }
        public ActionResult RemoveFromCart(int id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == id);
            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }


            return RedirectToAction("Index");
        }

        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }

        public PartialViewResult Summary1()
        {
            return PartialView(GetCart());
        }
        //SHİPPİNG DETAİLS

        public ActionResult CheckOut()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ActionResult Checkout(ShippingDetails model)//sepetten siparişe geçme saveorder metodu ile.
        {
            var cart = GetCart();
            model.Username = User.Identity.Name;
            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("Ürün Yok", "Sepetinizde ürün bulunmamaktadır.");
            }
            if (ModelState.IsValid)
            {
                SaveOrder(cart, model);//bir alttaki metodu çağırdık. 
                cart.Clear();
                return View("SiparisTamamlandi");
            }
            else
            {
                return View(model);
            }


        }
        private void SaveOrder(Cart cart, ShippingDetails model)
        {
            var order = new Order();

            order.Adres = model.Adress;
            order.OrderNumber = "A" + (new Random()).Next(1111, 9999).ToString(); // A1234 şeklinde random sipariş numarası yaratır.
            order.Total = cart.Total();//metot
            order.OrderDate = DateTime.Now;
            order.OrderState = OrderState.Bekleniyor;//sipariş durumu enum
            order.UserName = model.Username;
            order.Sehir = model.City;
            order.Ilce = model.Ilce;
            order.Mahalle = model.Mahalle;
            order.OrderLines = new List<OrderLine>();
            foreach (var item in cart.CartLines)
            {
                var orderline = new OrderLine();
                orderline.Quantity = item.Quantity;
                orderline.Price = item.Quantity * item.Product.Price;
                orderline.ProductId = item.Product.Id;
                order.OrderLines.Add(orderline);
            }
            db.Orders.Add(order);
            db.SaveChanges();

        }
    }
}