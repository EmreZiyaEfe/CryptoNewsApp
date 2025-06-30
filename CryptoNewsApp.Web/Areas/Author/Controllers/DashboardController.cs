using Microsoft.AspNetCore.Mvc;

namespace CryptoNewsApp.Web.Areas.Author.Controllers
{
    [Area("Author")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
