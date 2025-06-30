using CryptoNewsApp.Application.Dtos;
using CryptoNewsApp.Application.Services;
using CryptoNewsApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CryptoNewsApp.Web.Controllers
{
    public class HomeController : Controller
    {
        //Servis katmanını kullanmak için d-i ile alıyoruz
        private readonly INewsService _newsService;

        public HomeController(INewsService newsService)
        {
            _newsService = newsService;
        }
        // GET: /News
        //Tüm haberleri listeler
        // Herkes erişebilsin
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //Servisten son 20 haber
            var newList = await _newsService.FetchNewsFromApiAsync(10);
            if (!newList.Any())
            {
                // Logger veya breakpoint ile kontrol et
                Console.WriteLine("API’den haber gelmedi.");
            }
            return View(newList.ToList());
        }

    }
}