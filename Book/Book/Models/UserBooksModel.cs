using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Book.Models;
namespace Book.Models
{
    public class UserBooksModel: BaseBookModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        [Display(Name ="Make it available for renting")]
        public bool IsAvailable { get; set; }

        public UserModel User { get; set; }

        public int UserId { get; set; }
    }
}
