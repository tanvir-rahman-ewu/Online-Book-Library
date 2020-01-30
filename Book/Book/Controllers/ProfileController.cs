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

namespace Book.Controllers
{
    public class ProfileController : Controller
    {

        private readonly BookContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileController(BookContext bookContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = bookContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var usersList = await _context.User.ToListAsync();

            var user = (from individualUser in usersList
                                   where individualUser.Id.ToString() == _httpContextAccessor.HttpContext.Session.GetString("UserId") 
                                   select individualUser).FirstOrDefault();
            return View(user);
        }


    }
}
