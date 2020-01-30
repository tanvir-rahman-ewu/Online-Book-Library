using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Book.Models;

namespace Book.Controllers
{
    
    public class UserController : Controller
    {

        private readonly BookContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;

        //Constructor
        public UserController(BookContext context,
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

        public async Task<IActionResult> Order(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.SellBook
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderModel O = new OrderModel();       

            var book = await _context.SellBook
                .FirstOrDefaultAsync(m => m.Id == id);

          
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        List<OrderModel> li = new List<OrderModel>();

        [HttpPost]
        public ActionResult AddToCart(SellBookModel book, int qty)
        {
            OrderModel O = new OrderModel();

            O.ProductName = book.Name;
            O.Quantity = qty;
            O.Price = book.Price;
            O.Bill = qty * O.Price;
          
       
            if (ViewData["cart"] == null)
            {

                li.Add(O);
               
                ViewData["cart"] = li;

            }
            else
            {
                List<OrderModel> li2 = ViewBag.cart as List<OrderModel>;
                int flag = 0;

                foreach (var item in li2)
                {
                    if (item.ProductName == O.ProductName)
                    {
                        item.Quantity += O.Quantity;
                        item.Bill += O.Bill;
                        flag = 1;
                    }
                }

                if (flag == 0)
                {
                    li2.Add(O);
                }

                ViewData["cart"] = li2;
            }
       
            return RedirectToAction("Index","Home");
        }


        public IActionResult CheckOut()
        {
            return View();
        }
    }
}