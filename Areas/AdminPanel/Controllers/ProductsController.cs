using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrnekSite.Entity;
using OrnekSite.WebUI.Areas.AdminPanel.Helpers;

namespace OrnekSite.Areas.AdminPanel.Controllers
{
    [Authorize(Roles ="admin")]
    public class ProductsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: AdminPanel/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: AdminPanel/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: AdminPanel/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categoris, "Id", "Name");
            return View();
        }

        // POST: AdminPanel/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Image,Price,Stok,Slider,IsHome,IsFeatured,IsApproved,CategoryId")] Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                product.Image = UploadHelper.SaveImage(image);
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categoris, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: AdminPanel/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categoris, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: AdminPanel/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Image,Price,Stok,Slider,IsHome,IsFeatured,IsApproved,CategoryId")] Product product, HttpPostedFileBase editImage)
        {
            if (ModelState.IsValid)
            {
                product.Image = UploadHelper.SaveImage(editImage);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categoris, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: AdminPanel/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: AdminPanel/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
