using OrnekSite.Entity;
using OrnekSite.Entity.AdminOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrnekSite.Areas.AdminPanel.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
       
        DataContext db = new DataContext();
        // GET: AdminPanel/Admin
        public ActionResult Index()
        {
            StateModel model = new StateModel();
            model.BekleyenSiparisSayisi = db.Orders.Where(i => i.OrderState == Entity.ShippingDetails.OrderState.Bekleniyor).ToList().Count();
            model.TamamlananSiparisSayisi = db.Orders.Where(i => i.OrderState == Entity.ShippingDetails.OrderState.Tamamlandı).ToList().Count();
            model.PaketlenenSiparisSayisi = db.Orders.Where(i => i.OrderState == Entity.ShippingDetails.OrderState.Paketlendi).ToList().Count();
            model.KargolananSiparisSayisi = db.Orders.Where(i => i.OrderState == Entity.ShippingDetails.OrderState.Kargolandı).ToList().Count();
            model.UrunSayisi = db.Products.ToList().Count();
            model.SiparisSayisi = db.Orders.ToList().Count();

            return View(model);
        }

        public PartialViewResult NotificationMenu()
        {
            var notifications = db.Orders.Where(i => i.OrderState == Entity.ShippingDetails.OrderState.Bekleniyor);

            return PartialView(notifications);
        }
     
    }
}