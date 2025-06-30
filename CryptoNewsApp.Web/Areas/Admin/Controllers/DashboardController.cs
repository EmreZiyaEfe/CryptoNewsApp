using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoNewsApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            //Admin paneli anasayfa
            return View();
        }

        //Kullanıcı yönetimi raporlar veya sistem ayarları gibi işlemler
        public IActionResult UserManagement()
        {
            return View();
        }
    }
}
