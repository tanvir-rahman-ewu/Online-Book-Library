using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class SellBookModel : BaseBookModel
    {

        [Required(ErrorMessage = "Please Enter Quantity")]
        public int Quantity { get; set; }
        
        [Required(ErrorMessage = "Please Enter Price")]
        public int Price { get; set; }
    }
}
