using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Book.Controllers
{
    public class LiveChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}