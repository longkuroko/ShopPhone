using ShopPhone.Models;
using ShopPhone.Models.PhoneModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopPhone.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        //dang nhap
        [HttpPost]
        public ActionResult Login(Customer customer)
        {
            var login = db.Customers.Where(s => s.Username == customer.Username && s.Password == customer.Password).FirstOrDefault();

            if (login == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                return View("Login", "Customers");

            }
            else
            {
                Session["Customer"] = login;
                ViewBag.Thongbao = "Đăng nhập thành công";
                return RedirectToAction("Index", "Home");
            }
        }

        //ho so
        public ActionResult ProfileName()
        {
            if (Session["UserLogin"] != null)
            {
                ViewBag.Profile = ((Customer)Session["UserLogin"]).Username;
                return PartialView();
            }
            ViewBag.Profile = "Đăng nhập/ Đăng ký";
            return PartialView();
        }
        //dang xuat
        public ActionResult LogOut()
        {
            Session["Customer"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }
        //dang ky
        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var check = db.Customers.FirstOrDefault(s => s.Username == customer.Username);
                if (check == null)
                {
                    Customer customer1 = new Customer()
                    {
                        fullname = customer.fullname,
                        Username = customer.Username,
                        Password = customer.Password,
                        Email = customer.Email,
                        Address = customer.Address,
                        Phone = customer.Phone

                    };
                    db.Customers.Add(customer1);
                    db.SaveChanges();
                    ViewBag.ThongBao = "Đăng nhập thành công";
                    return RedirectToAction("Login", "Customers");
                }
            }
            else
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại!";
                return View();
            }

            return View();

        }


        //Xem chi tiết ngườ dùng
        public ActionResult Details(int? id)
        {
            int customerId = id ?? default(int);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //cập nhật thông tin
        [HttpPost]
        public JsonResult UpdateInfo(int id, string fullName, string address, string phone, string email)
        {

            var customer = db.Customers.Where(x => x.Id == id).FirstOrDefault();
            customer.fullname = fullName;

            customer.Address = address;
            customer.Phone = phone;
            customer.Email = email;

            UpdateModel(customer);
            db.SaveChanges();

            Session["Customer"] = customer;

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        //cập nhật mậ khẩu
        [HttpPost]
        public JsonResult ChangePwd(int id, string newPwd)
        {
            var customer = db.Customers.Where(x => x.Id == id).FirstOrDefault();
            if (customer != null)
            {
                customer.Password = newPwd;
                UpdateModel(customer);
                db.SaveChanges();
                Session["Customer"] = customer;
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("CannotFindCustomer", JsonRequestBehavior.AllowGet);
            }
        }
    }
}