using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ADDyourAD.Models;

namespace ADDyourAD.Controllers
{
    public class HomeController : Controller
    {

        private readonly AdvertisementDBContext _context;

        public HomeController(AdvertisementDBContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {

            return Redirect("~/Advertisement/Index");
        }


        [HttpPost]
        public IActionResult Search(string searchString)
        {
            var advertisementDBContext = _context.Advertisement.Include(a => a.IdCategoryNavigation).Include(a => a.IdUserNavigation);
            var advertisements = new List<Advertisement>();
            if (!String.IsNullOrEmpty(searchString))
            {
                advertisements = advertisementDBContext.Where(a => (a.Title).Contains(searchString) 
                || (a.Details).Contains(searchString) || 
                (a.IdCategoryNavigation.CategoryName).Contains(searchString)
                || (a.IdUserNavigation.Username).Contains(searchString)).ToList();
            }

            return View(advertisements);
        }
      
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
