using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventryManagementSystem.Models
{
    /// <summary>
    /// This is Product class , encapsulation product details
    /// </summary>
    public class Prod
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Product Enter here!")]
        [Remote("IsProductNameisExist", "Product", ErrorMessage = "Product is already Exist")]
        public string product_name { get; set; }

        [Required(ErrorMessage = "Quentity Enter here!")]
        public String product_qnty { get; set; }
    }
}