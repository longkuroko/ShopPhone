using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopPhone.Models.PhoneModel
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Hãng")]
        public string Name { get; set; }

        public ICollection<Phone> Phones { get; set; }

        public Brand()
        {
            Phones = new Collection<Phone>();
        }
    }
}