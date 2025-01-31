using Microsoft.AspNetCore.Mvc;

namespace PB202_Pronia.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
