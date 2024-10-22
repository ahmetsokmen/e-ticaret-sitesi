
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrnekSite.Entity;
using OrnekSite.Entity.ViewModels;

namespace OrnekSite.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();

        public ActionResult Search(string search)
        {
            var products = db.Products.ToList().Where(x=>x.IsApproved==true);
            List<Product> searchedProduct = new List<Product>();
            foreach (var item in products)
            {
                if (!string.IsNullOrEmpty(search))
                {

                    if (item.Name.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) >=0 || item.Description.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) >=0)//büyük küçük harf duyarlılığını kaldırma
                    { 
                        searchedProduct.Add(item);

                    }

                }
            }
            return View(searchedProduct);
        }
        public PartialViewResult FeaturedProduct()
        {
            var products = db.Products.ToList();
            return PartialView(products.Where(x=>x.IsApproved==true && x.IsFeatured==true).Take(5).ToList());
        }
        public PartialViewResult CategoryList()///////Önemli
        {
            var model = db.Categoris.Select(x => new CategoryListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Count = x.Products.Count()//her bir kategoriye ait propertyleri viewmodeldeki propertylere aktardık.
            }).ToList();
            
           
            return PartialView(model);
        }
        public ActionResult Index()
        {
            return View(db.Products.Where(x=>x.IsHome==true && x.IsApproved==true).ToList());
        }

        public ActionResult ProductDetails(int? id)
        {
           

            return View(db.Products.Where(x=>x.Id==id).FirstOrDefault());
        }

        public ActionResult Product()
        {
           

            return View(db.Products.Where(x => x.IsApproved == true).ToList());
        }
        public ActionResult GetItemsFromCategory(int id)
        {
            var products = db.Products.Where(x => x.Category.Id == id && x.IsApproved == true).ToList();
            return View(products);
        }
     
       
    }
}