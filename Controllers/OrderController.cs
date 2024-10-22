using OrnekSite.Entity;
using OrnekSite.Entity.AdminOrder;
using OrnekSite.Entity.ShippingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrnekSite.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        DataContext db = new DataContext();
        // GET: Order
        public ActionResult Index()
        {
            var orders = db.Orders.Select(i => new AdminOrder

            {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Total = i.Total,
                Count = i.OrderLines.Count

            }).OrderByDescending(i=>i.OrderDate).ToList();//son verilen sipariş en üstte görünsün.
            return View(orders);
        }
        public ActionResult Details(int id)//yeni model oluşturmadık var olan order details modelini kullandık.
        {
            var model = db.Orders.Where(i => i.Id == id).Select(i => new OrderDetails
            {

                Adres = i.Adres,
                Ilce = i.Ilce,
                Mahalle = i.Mahalle,
                OrderDate = i.OrderDate,
                OrderId = i.Id,
                OrderNumber = i.OrderNumber,
                OrderState = i.OrderState,
                Sehir = i.Sehir,
                Total = i.Total,
                UserName = i.UserName,
                OrderLines = i.OrderLines.Select(x => new OrderLineModel()
                {
                    ProductId = x.ProductId,
                    Image = x.Product.Image,
                    Price = x.Product.Price,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity

                }).ToList()

            }).FirstOrDefault() ;

          
            return View(model);
        }

      
        public ActionResult UpdateOrderState(int OrderId, OrderState Orderstate)
        {

            var order = db.Orders.FirstOrDefault(i => i.Id == OrderId);
            if (order!=null)
            {
                order.OrderState = Orderstate;
                db.SaveChanges();
                ViewBag.Message11 = "Sipariş Durumu Güncellendi.";
                return RedirectToAction("Details", new {id=OrderId });
            }

            return RedirectToAction("Index");

        }

        public ActionResult BekleyenSiparisler()
        {
            var model = db.Orders.Where(x => x.OrderState == OrderState.Bekleniyor).ToList();
            return View(model);
        }
        public ActionResult TamamlananSiparisler()
        {
            var model = db.Orders.Where(x => x.OrderState == OrderState.Tamamlandı).ToList();
            return View(model);
        }
        public ActionResult PaketlenenSiparisler()
        {
            var model = db.Orders.Where(x => x.OrderState == OrderState.Paketlendi).ToList();
            return View(model);
        }

        public ActionResult KargolananSiparisler()
        {
            var model = db.Orders.Where(x => x.OrderState == OrderState.Kargolandı).ToList();
            return View(model);
        }
    }
}