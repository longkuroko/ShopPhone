using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopPhone.Models;
using ShopPhone.Models.PhoneModel;

namespace ShopPhone.Areas.Admin.Controllers
{
    [Authorize]
    public class PhonesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Phones
        public ActionResult Index()
        {
            var phones = db.Phones.Include(p => p.Brand);
            return View(phones.ToList());
        }

        // GET: Admin/Phones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // GET: Admin/Phones/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
            return View();
        }

        // POST: Admin/Phones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Image,Details,BrandId,CPU,Ram,ManHinh")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Phones.Add(phone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", phone.BrandId);
            return View(phone);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Price,Image,Details,BrandId,CPU,Ram,ManHinh")] Phone product, HttpPostedFileBase fileupload)
        //{

        //    ViewBag.CategoryId = new SelectList(db.Brands, "Id", "Name");
        //    if (fileupload != null)
        //    {
        //        string filename = Path.GetFileName(fileupload.FileName);
        //        string path = Server.MapPath("~/UploadFile/" + filename);
        //        fileupload.SaveAs(path);
        //        product.Image  = "UploadFile/" + filename;

        //    }
        //    if (ModelState.IsValid)
        //    {

        //        db.Phones.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(product);
        //}

        // GET: Admin/Phones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", phone.BrandId);
            return View(phone);
        }

        // POST: Admin/Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Image,Details,BrandId,CPU,Ram,ManHinh")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", phone.BrandId);
            return View(phone);
        }

        // GET: Admin/Phones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // POST: Admin/Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phone phone = db.Phones.Find(id);
            db.Phones.Remove(phone);
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
