using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QTF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTF.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly QtfDbContext _context;

        public AdminController(QtfDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Roles()
        {
            

            return View();
        }
    }
}