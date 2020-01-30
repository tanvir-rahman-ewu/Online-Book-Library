using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Book.Models;
using Book.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Book.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookContext _context;

        //constructor
        public HomeController(BookContext bookContext)
        {
            _context = bookContext;
        }

        public IActionResult Index()
        {
           /* if (ViewData["cart"] != null)
            {
                float x = 0;
                int ct = 0;
                List<OrderModel> li2 = TempData["cart"] as List<OrderModel>;
                foreach (var item in li2)
                {
                    x += item.Bill;
                    ct++;

                }

                ViewData["total"] = x;
                ViewData["cartto"] = ct;
            }*/
            
            var query = from book in _context.SellBook
                        select book;

            var books = query.ToList();

            return View(books);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult UsersBook()
        {
            var query = from book in _context.Books
                        select book;

            var books = query.ToList();

            return View(books);
           
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("ID,FirstName,LastName,Dob,Address,Email,Phone,Password,ConfirmPassword")] UserModel user, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                user.ProfilePicUrl = UploadFile(file, user.Id);
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public string  UploadFile(IFormFile file, int UserId)
        {
            var fileName = file.FileName;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Profile_Pictures", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
            
        }
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn([Bind("Email,Password")] LogInModel logInModel)
        {
            if (ModelState.IsValid)
            {
                var usersList = await _context.User.ToListAsync();

                var user = (from individualUser in usersList
                            where individualUser.Email == logInModel.Email && individualUser.Password == logInModel.Password
                            select individualUser).FirstOrDefault();

                if (user == null)
                {
                    ModelState.AddModelError("", "wrong credentials");
                    return View(logInModel);
                }
                else if(user.IsAdmin==true)
                {
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("Name", user.FirstName.ToString());

                    return RedirectToAction("Index","Admin");
                }
                else
                {
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("Name", user.FirstName.ToString());

                    return RedirectToAction("Index","Home");
                }

            }
            return View(logInModel);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Name");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
