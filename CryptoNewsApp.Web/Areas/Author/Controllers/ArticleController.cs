using Microsoft.AspNetCore.Mvc;

namespace CryptoNewsApp.Web.Areas.Author.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
