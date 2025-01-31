using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB202_Pronia.Contexts;
using PB202_Pronia.ViewModels;

namespace PB202_Pronia.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var categories = _context.Categories.Include(x => x.Products).ThenInclude(x => x.ProductImages).Take(3).ToList();

            HomeVM vm = new()
            {
                Categories = categories,
            };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}
