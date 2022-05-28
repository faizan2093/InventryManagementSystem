using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventryManagementSystem.Models
{
    public class Sale
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Product Enter here!")]
        public String Sale_Product { get; set; }
        
        [Required(ErrorMessage = "Quentity Enter here!")]
        public int Sale_Qnty { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Sale_Date { get; set; }
    }
}