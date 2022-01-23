using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADDyourAD.Models;
using ADDyourAD;

namespace ADDyourAD.Controllers
{
    public class AccountController : Controller
    {
        private readonly AdvertisementDBContext _context;

        public AccountController(AdvertisementDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        { 
            return Redirect("~/Advertisement/Index");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        public IActionResult Logout()
        {
            Authentication.Instance.AccountLogout();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                Authentication.Instance.AccountLogin(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var name = user.Username;
            var dbAdmin = _context.Admin.FirstOrDefault(a => a.Name == name);
            if (dbAdmin != null && dbAdmin.Name == name)
            {
                if (dbAdmin.Password == user.Password)
                {
                    Authentication.Instance.AdminLogin(user);
                    await _context.SaveChangesAsync();
                    
                }

                return RedirectToAction(nameof(Index));
            }

            var dbUser = _context.User.FirstOrDefault(u => u.Username == user.Username);
            if (dbUser == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (dbUser.Password != user.Password)
            {
                return RedirectToAction(nameof(Index));
            }
            Authentication.Instance.AccountLogin(dbUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        
        }

       
    }
}