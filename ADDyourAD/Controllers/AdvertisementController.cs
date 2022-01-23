using ADDyourAD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace ADDyourAD.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly AdvertisementDBContext _context;

        public AdvertisementController(AdvertisementDBContext context)
        {
            _context = context;
        }


        
        public ViewResult Index(string sortOrder, bool includeExpired, string byCategory)
        {

            ViewBag.Categories = _context.Category.ToList();

            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : " ";
            ViewBag.UserSortParm = String.IsNullOrEmpty(sortOrder) ? "user_desc" : "user_asc";
            ViewBag.CategorySortParm = String.IsNullOrEmpty(sortOrder) ? "cat_desc" : "cat_asc";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var advertisementDBContext = _context.Advertisement.Include(a => a.IdCategoryNavigation).Include(a => a.IdUserNavigation);
          
          
            var advertisements = advertisementDBContext.ToList();

           

            switch (sortOrder)
            {
                case "user_desc":
                    advertisements = advertisementDBContext.OrderByDescending(a => a.IdUserNavigation.Username).ToList();
                    break;
                case "user_asc":
                    advertisements = advertisementDBContext.OrderBy(a => a.IdUserNavigation.Username).ToList();
                    break;
                case "cat_desc":
                    advertisements = advertisementDBContext.OrderByDescending(a => a.IdCategoryNavigation.CategoryName).ToList();
                    break;
                case "cat_asc":
                    advertisements = advertisementDBContext.OrderBy(a => a.IdCategoryNavigation.CategoryName).ToList();
                    break;
                case "date_desc":
                    advertisements = advertisementDBContext.OrderByDescending(a => a.AddDate).ToList();
                    break;
                case "Date":
                    advertisements = advertisementDBContext.OrderBy(a => a.AddDate).ToList();
                    break;
                case "title_desc":
                    advertisements = advertisementDBContext.OrderByDescending(a => a.Title).ToList();
                    break;
                default:
                    advertisements = advertisementDBContext.OrderBy(a => a.Title).ToList();
                    break;
            }

            if (!includeExpired)
            {
                advertisements.RemoveAll(a => !AdvertisementUtils.Instance.isNotExpired(a.ExpirationDate));
            }

            if (byCategory != null)
            {
                advertisements.RemoveAll(a => a.IdCategoryNavigation.CategoryName != byCategory);
            }


            return View(advertisements);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisement
                .Include(a => a.IdCategoryNavigation)
                .Include(a => a.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdAdvertisement == id);
            if (advertisement == null)
            {
                return NotFound();
            }
            AdvertisementUtils.Instance.setIdAd((int)id);      
            var Comments = 
                _context.Comment.Where(c => c.IdAdvertisement == id).Include(c=>c.IdUserNavigation);


            ViewBag.comments = Comments;

            return View(advertisement);
        }

        public IActionResult Create()
        {

            if (!Authentication.Instance.isLoggedIn())
            {
                return Redirect("~/Advertisement/Index");
            }
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "CategoryName");

            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAdvertisement,Title,Details,AddDate,ExpirationDate,IdUser,IdCategory")] Advertisement advertisement)
        {

            DateTime addDate = DateTime.Now;
            TimeSpan avaiability = new TimeSpan(14, 0, 0, 0);
            DateTime expirationDate = addDate.Add(avaiability);

            advertisement.AddDate = addDate;
            advertisement.ExpirationDate = expirationDate;

            

            User currentUser = Authentication.Instance.getCurrentUser();


            
            if(currentUser != null)
                advertisement.IdUser = currentUser.IdUser;
            else
                return View(advertisement);

            if (ModelState.IsValid)
            {
                _context.Add(advertisement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "CategoryName", advertisement.IdCategory);
            ViewData["IdUser"] = new SelectList(_context.User, "IdUser", "Username", advertisement.IdUser);
            return View(advertisement);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisement
                .Include(a => a.IdCategoryNavigation)
                .Include(a => a.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdAdvertisement == id);

            if (advertisement == null)
            {
                return NotFound();
            }
            AdvertisementUtils.Instance.setAddDate(advertisement.AddDate);
            AdvertisementUtils.Instance.setExpDate(advertisement.ExpirationDate);
            AdvertisementUtils.Instance.setUser(advertisement.IdUser);
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "CategoryName", advertisement.IdCategory);
            ViewData["IdUser"] = new SelectList(_context.User, "IdUser", "Username", advertisement.IdUser);

            if (!Authentication.Instance.isLoggedIn())
            {
                return Redirect("~/Advertisement/Index");
            }
            else if (!Authentication.Instance.isAdmin() &&
                          (Authentication.Instance.getCurrentUser()).Username != advertisement.IdUserNavigation.Username)
            {

                return Redirect("~/Advertisement/Index");

            }

            return View(advertisement);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("IdAdvertisement,Title,Details,AddDate,ExpirationDate,IdUser,IdCategory")] Advertisement advertisement) { 
            if (id != advertisement.IdAdvertisement)
            {
                return NotFound();
            }

            advertisement.AddDate = AdvertisementUtils.Instance.getAddDate();
            advertisement.ExpirationDate = AdvertisementUtils.Instance.getExpDate();

            
            advertisement.IdUser = AdvertisementUtils.Instance.getUser();

           

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(advertisement.IdAdvertisement))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "CategoryName", advertisement.IdCategory);
            ViewData["IdUser"] = new SelectList(_context.User, "IdUser", "Username", advertisement.IdUser);
            return View(advertisement);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisement
                .Include(a => a.IdCategoryNavigation)
                .Include(a => a.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdAdvertisement == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            if (!Authentication.Instance.isLoggedIn())
            {
                return Redirect("~/Advertisement/Index");
            }
            else if (!Authentication.Instance.isAdmin() &&
                          (Authentication.Instance.getCurrentUser()).Username != advertisement.IdUserNavigation.Username)
            {

                return Redirect("~/Advertisement/Index");

            }


            return View(advertisement);



        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await _context.Advertisement.FindAsync(id);
            var comments = _context.Comment.Where(c => c.IdAdvertisement == id).ToList();

            foreach (var item in comments)
            {
                _context.Remove(item);
            }

            await _context.SaveChangesAsync();
            _context.Advertisement.Remove(advertisement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(string comment)
        {
            if (!String.IsNullOrEmpty(comment))
            {
                Comment newComment = new Comment();
                newComment.Text = comment;
                newComment.IdAdvertisement = AdvertisementUtils.Instance.getIdAd();
                newComment.IdUser = Authentication.Instance.getCurrentUser().IdUser;
                newComment.Date = DateTime.Now;
                _context.Add(newComment);
                await _context.SaveChangesAsync();
            }

            return Redirect("~/Advertisement/Details/" + (AdvertisementUtils.Instance.getIdAd()).ToString());
        }


        private bool AdvertisementExists(int id)
        {
            return _context.Advertisement.Any(e => e.IdAdvertisement == id);
        }


     

    }
}
