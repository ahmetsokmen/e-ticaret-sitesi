using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrnekSite.Entity.ShippingDetails
{
    public class ShippingDetails
    {
        
        public string Username { get; set; }
        [Required(ErrorMessage = "Lütfen adres giriniz.")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Lütfen şehir giriniz.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Lütfen ilçe giriniz.")]
        public string Ilce { get; set; }
        [Required(ErrorMessage = "Lütfen mahalle giriniz.")]
        public string Mahalle { get; set; }
       


    }
}