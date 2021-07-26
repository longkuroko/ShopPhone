using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ShopPhone.Models;
using ShopPhone.Models.PhoneModel;

namespace ShopPhone.Controllers
{
    public class PhonesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Phones
        public ActionResult Index(string Search, int? page)
        {
            var phones = db.Phones.Include(p => p.Brand).ToList();
            if (!String.IsNullOrEmpty(Search))
            {
                ViewBag.Search = Search;
                phones = phones.Where(p => p.Name.ToLower().Contains(Search.ToLower())).ToList();
            }
            return View(phones.ToPagedList(page ?? 1, 9));
        }

        // GET: Phones/Details/5
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


      
    }
}
