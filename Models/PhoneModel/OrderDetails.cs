using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopPhone.Models.PhoneModel
{
    public class OrderDetails
    {
        public int Id { get; set; }

        [Display(Name = "Mã sản phẩm")]
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
        [Display(Name = "Số lượng")]
        public int Qty { get; set; }
        [Display(Name = "Đơn giá")]
        public decimal Price { get; set; }

        [Display(Name = "Mã đơn hàng")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}