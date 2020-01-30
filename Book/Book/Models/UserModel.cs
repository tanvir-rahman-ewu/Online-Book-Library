using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class UserModel
    {
        public UserModel()
        {
            Books = new List<UserBooksModel>();
        }
        public int Id { get; set; }

        [Required, MaxLength(256)]
        public string FirstName { get; set; }

        [Required, MaxLength(256)]
        public string LastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Required, DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [Required, MaxLength(256)]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public string ProfilePicUrl { get; set; }

    
        public bool IsAdmin { get; set; }

        public List<UserBooksModel> Books { get; set; }

    }
}
