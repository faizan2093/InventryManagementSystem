using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventryManagementSystem.Models
{
    public class Purc
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Enter Product Name")]        
        public String Purchase_Product { get; set; }

        [Required(ErrorMessage = "Enter Quentity")]
        public int Purchase_Qnty { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date_Of_Purchase { get; set; }
    }
}