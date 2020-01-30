using Book.Data;
using Book.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Controllers
{
    public class BooksController: Controller
    {
        private readonly BookContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public BooksController(BookContext context,
                                IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            var query = from book in _context.Books
                        where book.UserId.ToString() == userId
                        select book;
            var books = query.ToList();

            return View(books);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserBooksModel model)
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            var query = from user in _context.User
                        where user.Id.ToString() == userId
                        select user;
            var currentUser = query.FirstOrDefault();

            if (ModelState.IsValid)
            {
                //currentUser.Books.Add(model);
                //_context.User.Update(currentUser);
                //_context.SaveChanges();

                model.User = currentUser;
                model.UserId = currentUser.Id;
                _context.Books.Add(model);
                _context.SaveChanges();

                var redirectUrl = Url.Action("Index","Books",HttpContext.Request.Scheme);
                return Redirect(redirectUrl);
            }
            return View(model);
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
