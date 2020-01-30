using Book.Data;
using Book.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Controllers
{
    public class AdminController: Controller
    {
        private readonly BookContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;

        //Constructor
        public AdminController(BookContext context,
                                IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var query = from book in _context.SellBook
                        select book;

            var books = query.ToList();

            return View(books);
        }

        public IActionResult ShowAllUser()
        {
            var query = from user in _context.User
                        select user;

            var users = query.ToList();

            return View(users);
        }

        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(SellBookModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                model.ProfilePicUrl = UploadFile(file, model.Id);

                _context.SellBook.Add(model);

                 await _context.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public string UploadFile(IFormFile file, int UserId)
        {
            var fileName = file.FileName;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Book_cover", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;

        }

    }
}
