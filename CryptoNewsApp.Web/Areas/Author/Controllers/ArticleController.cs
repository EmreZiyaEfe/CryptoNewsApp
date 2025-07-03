using Microsoft.AspNetCore.Mvc;

namespace CryptoNewsApp.Web.Areas.Author.Controllers
{
    [Area("Author")]
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
