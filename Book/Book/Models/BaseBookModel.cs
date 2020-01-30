using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class BaseBookModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        [Display(Name = "Publish Date")]
        public string PublishedDate { get; set; }

        public string ProfilePicUrl { get; set; }

    }
}
