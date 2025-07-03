using CryptoNewsApp.Application.Services;
using CryptoNewsApp.Core.Entities;
using CryptoNewsApp.Core.Enums;
using CryptoNewsApp.Web.Areas.Admin.Models.Vms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CryptoNewsApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Tüm kategorileri veritabanından alıyoruz
            var categories = await _categoryService.GetAllCategoriesAsync();

            // 2. Kategori varlıklarını CategoryListVm listesine dönüştürüyoruz
            var categoryVmList = categories.Select(c => new CategoryListVm
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug,
                Description = c.Description,
                Status = c.Status,
            }).ToList();

            // 3. Enum'dan select list oluşturuyoruz (CategoryStatus enum'ı için)
            var statusList = Enum.GetValues(typeof(CategoryStatus)) // enum değerlerini al
                .Cast<CategoryStatus>() // her bir enum değerini türüne dök
                .Select(cs => new SelectListItem
            {
                Value = ((int)cs).ToString(), // enum int değeri
                    Text = cs.ToString() // enum adı (Active, Inactive vs.)
            })
                .ToList();

            // 4. ViewModel'e tüm dataları atıyoruz
            var indexVm = new CategoryIndexVm
            {
                Categories = categoryVmList,
                CreateCategory = new CreateCategoryVm
                {
                    Statuses = statusList, // dropdown için

                }
            };


            //var categoryEntities = await _categoryService.GetAllCategoriesAsync();

            //var categories = categoryEntities.Select(c => new CategoryListVm
            //{
            //    Id = c.Id,
            //    Name = c.Name,
            //    Description = c.Description,
            //    Status = c.Status,
            //}).ToList();

            //var indexVm = new CategoryIndexVm
            //{
            //    Categories = categories,
            //    CreateCategory = new CreateCategoryVm()
            //};
           
            return View(indexVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryIndexVm model)
        {
            // 1. ModelState kontrolü: Kullanıcıdan gelen form verileri geçerli mi?
            if (!ModelState.IsValid)
            {

                Console.WriteLine("ModelState geçersiz!!");
                foreach (var kvp in ModelState)
                {
                    foreach (var error in kvp.Value.Errors)
                    {
                        Console.WriteLine($"KEY: {kvp.Key} - ERROR: {error.ErrorMessage}");
                    }
                }

                // 2. Status listesini tekrar doldur (çünkü sayfa yeniden yüklenecek)
                model.CreateCategory.Statuses = Enum.GetValues(typeof(CategoryStatus))
                    .Cast<CategoryStatus>()
                    .Select(cs => new SelectListItem
                {
                    Value = ((int)cs).ToString(),
                    Text = cs.ToString()
                }).ToList();

                // 3. Tüm kategorileri tekrar al ve ViewModel'e ekle
                var categoryEntities = await _categoryService.GetAllCategoriesAsync();

                model.Categories = categoryEntities.Select(c => new CategoryListVm
                {
                    Id = c.Id,
                    Name = c.Name,
                    Slug = c.Slug,
                    Description = c.Description,
                    Status = c.Status
                }).ToList();

                // 4. Index view'ını model ile birlikte tekrar göster (form hataları gösterilir)
                return View("Index", model);
            }

            var slug = model.CreateCategory.Name.ToLower().Replace(" ", "-");

            var category = new Category
            {
                Name = model.CreateCategory.Name,
                Description = model.CreateCategory.Description,
                Status = model.CreateCategory.Status,
                Slug = slug
            };

            // 7. Veritabanına kaydet
            await _categoryService.CreateCategoryAsync(category);

            // 8. Kullanıcıya bilgi vermek için TempData ile mesaj gönder
            TempData["Success"] = "Category added successfully.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // 1. Kategoriyi veritabanından çek
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            // 2. Status listesini hazırla

            var statuses = Enum.GetValues(typeof(CategoryStatus)).Cast<CategoryStatus>().Select(cs => new SelectListItem
            {
                Value = ((int)cs).ToString(),
                Text = cs.ToString()
            }).ToList();

            var vm = new EditCategoryVm
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Status = category.Status,
                Statuses = statuses
            };

            return Json(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryVm model)
        {
            if (!ModelState.IsValid)
            {
                model.Statuses = Enum.GetValues(typeof(CategoryStatus))
                    .Cast<CategoryStatus>()
                    .Select(cs => new SelectListItem
                    {
                        Value = ((int)cs).ToString(),
                        Text = cs.ToString()
                    }).ToList();

                return View("Index", model);
            }

            var category = await _categoryService.GetCategoryByIdAsync(model.Id);
            var slug = model.Name.ToLower().Replace(" ", "-");
            if (category == null) return NotFound();

            category.Name = model.Name;
            category.Slug = slug;
            category.Description = model.Description;
            category.Status = model.Status;

            await _categoryService.UpdateCategoryAsync(category);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                TempData["Error"] = "Category is not found for remove.";
                return RedirectToAction("Index");
            }

            await _categoryService.DeleteCategoryAsync(id);
            TempData["Success"] = "The category has been remove succesfully.";
            return RedirectToAction("Index");

        }

    }
}
