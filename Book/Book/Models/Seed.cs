using Book.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class Seed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BookContext>>()))
            {
                // Look for any movies.
                if (context.User.Any())
                {
                    return;   // DB has been seeded
                }

                context.User.AddRange(

                    new UserModel
                    {
                        FirstName = "Admin",
                        LastName ="Y",
                        Dob = DateTime.Parse("1959-4-15"),
                        Address = "ABC",
                        Email = "admin@gmail.com",
                        Phone ="1234",
                        Password ="admin",
                        ConfirmPassword ="admin",
                        IsAdmin =true
                    }
                );
                
                context.SaveChanges();
            }
        }
    }
}
