using ShopPhone.Models;
using ShopPhone.Models.PhoneModel;
using ShopPhone.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopPhone.Controllers
{
    public class ShopingCartController : Controller
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
        static List<Cart> listCartItem = new List<Cart>();

        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        //them san pham vao gio hang
        public ActionResult AddToCart(int id)
        {
            if (Session["Customer"] == null)
            {
                return RedirectToAction("Login", "Customers");
            }
            else
            {
                var product = _dbContext.Phones.SingleOrDefault(s => s.Id == id);
                if (product != null)
                {
                    GetCart().AddToCart(product);
                }
                return RedirectToAction("ShowtoCart", "ShopingCart");
            }

        }
        //xem gio hang
        public ActionResult ShowtoCart()
        {
            if (Session["Customer"] == null)
            {
                return RedirectToAction("Login", "Customers");
            }
            if (Session["Cart"] == null)
            {
                return RedirectToAction("ShowtoCart", "ShopingCart");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        //update so luong gio hang
        public ActionResult UpdateQuantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int Idproduct = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);

            cart.UpdateQty(Idproduct, quantity);
            return RedirectToAction("ShowtoCart", "ShopingCart");
        }

        //remove item
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove(id);

            return RedirectToAction("ShowtoCart", "ShopingCart");
        }

        public PartialViewResult BagCart()
        {
            int totalItem = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)

                totalItem = cart.TotalQuantity();
            ViewBag.QuantityCart = totalItem;
            return PartialView("BagCart");

        }
        public ActionResult Checkout()
        {
            Cart cart = Session["Cart"] as Cart;
            if (Session["Customer"] == null || Session["Customer"].ToString() == "")
            {
                return RedirectToAction("Login", "Customers");
            }
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Product");
            }

            CheckoutViewModel viewModel = new CheckoutViewModel
            {
                customer = (Customer)Session["Customer"],
                Cart = Session["Cart"] as Cart
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Checkout(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Customer customer = (Customer)Session["Customer"];
                Order order = new Order();

                var tongtien = cart.TotalMoney().ToString();
                order.CustomerId = customer.Id;
                order.OrderDay = DateTime.Now;
                order.OrderTotal = Convert.ToDecimal(tongtien);
                _dbContext.Orders.Add(order);
                foreach (var item in cart.CartItems)
                {
                    OrderDetails orderDetail = new OrderDetails();
                    orderDetail.OrderId = order.Id;
                    orderDetail.PhoneId = item.Phone.Id;
                    orderDetail.Price = item.Phone.Price;
                    orderDetail.Qty = item.qty;
                    _dbContext.OrderDetails.Add(orderDetail);
                }
                _dbContext.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("OrderSuccess", "ShopingCart");
            }
            catch
            {
                return Content("Kiểm tra lại thông tin khách hàng");
            }
        }

        public ActionResult OrderSuccess()
        {
            return View();
        }

    }
}