
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopPhone.Models.PhoneModel
{
    public class Phone
    {
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Gía")]
        public decimal Price { get; set; }
        [Display(Name = "Ảnh sản phẩm")]
        public string Image { get; set; }
        [Display(Name = "Chi tiết sản phẩm")]
        public string Details { get; set; }
        [Display(Name = "Thương hiệu")]
        public Brand Brand { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public string CPU { get; set; }
        public string Ram { get; set; }
        [Display(Name = "Màn hình")]
        public string ManHinh { get; set; }
        //public string Color { get; set; }

    }
}