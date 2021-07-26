using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopPhone.Models.PhoneModel
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ngày đặt hàng")]
        public DateTime OrderDay { get; set; }


        [Display(Name = "Tổng đơn hàng")]
        public decimal OrderTotal { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}