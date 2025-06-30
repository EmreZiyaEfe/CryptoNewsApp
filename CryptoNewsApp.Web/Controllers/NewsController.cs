using CryptoNewsApp.Application.Services;
using CryptoNewsApp.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoNewsApp.Web.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        //Servis katmanını kullanmak için d-i ile alıyoruz
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
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
            var newList = await _newsService.GetLatestNewsAsync(20);
            return View(newList);
        }

        // Tek bir haberi detay sayfasında gösterir
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            // GET: /News/Details/5
            // Id ile haberi servisten alıyoruz
            var news = await _newsService.GetNewsByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // GET: /News/Create
        // Yeni haber eklemek için formu gösterir
        public IActionResult Create()
        {
            return View();
        }

        // POST: /News/Create
        // Formdan gelen verilerle yeni haber oluşturur
        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF saldırılarına karşı koruma
        public async Task<IActionResult> Create(News news)
        {
            if (ModelState.IsValid) //Model doğrulaması başarılı ise 
            {
                await _newsService.CreateNewsAsync(news); // Haber ekleme servisini çağırıyoruz
                return RedirectToAction(nameof(Index)); // İşlem sonrası liste sayfasına yönlendir
            }
            return View(news); // Model geçersiz ise formu tekrar göster
        }

        // GET: /News/Edit/5
        // Haber düzenleme formunu gösterir
        public async Task<IActionResult> Edit(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: /News/Edit/5
        // Düzenlenen haber bilgilerini kaydeder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, News news)
        {
            if(id != news.Id)
            {
                return BadRequest(); // Id uyuşmazlığı varsa hata döner
            }

            if (ModelState.IsValid)
            {
                await _newsService.UpdateNewsAsync(news); //Güncelleme işlemi
                return RedirectToAction(nameof(Index));
            }
            return View(news); // Model geçersizse tekrar düzenleme formu gösterilir

        }

        // GET: /News/Delete/5
        // Silme onayı için haber detayını gösterir
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            if(news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: /News/Delete/5
        // Haberi siler
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _newsService.DeleteNewsAsync(id); //silme servisi çağrlır
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("fetch-only")]
        public async Task<IActionResult> FetchFromApi()
        {
            var newDtos = await _newsService.FetchNewsFromApiAsync(10);

            return Ok(newDtos);
        }

        [HttpPost("fetch-and-save")]
        public async Task<IActionResult> FetchAndSave()
        {
            var addedCount = await _newsService.SaveNewsFromApiAsync(10);
            return Ok($"{addedCount} haber kaydedildi");
        }
    }
}
