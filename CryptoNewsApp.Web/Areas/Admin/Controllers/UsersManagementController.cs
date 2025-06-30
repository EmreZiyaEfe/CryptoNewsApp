using CryptoNewsApp.Application.Dtos;
using CryptoNewsApp.Application.Interfaces;
using CryptoNewsApp.Core.Entities;
using CryptoNewsApp.Core.Enums;
using CryptoNewsApp.Core.UnitOfWork;
using CryptoNewsApp.Infrastructure.Services;
using CryptoNewsApp.Web.Areas.Admin.Models.Vms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CryptoNewsApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class UsersManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;

        public UsersManagementController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userVmList = new List<UsersVm>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "User";

                userVmList.Add(new UsersVm
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = role,
                    JoinDate = user.CreatedAt,
                    Status = user.Status,
                });

            }
            var rolesList = _roleManager.Roles
                   .Select(x => new SelectListItem
                   {
                       Value = x.Name,
                       Text = x.Name
                   })
                   .ToList();

            var statusList = Enum.GetValues(typeof(UserStatus)).Cast<UserStatus>().Select(s => new SelectListItem
            {
                Value = ((int)s).ToString(),
                Text = s.ToString()

            })
                .ToList();
            var indexVm = new UserIndexVm
            {
                Users = userVmList,
                AddUser = new AddUserVm
                {
                    Roles = rolesList,
                    Statuses = statusList
                }
            };

            return View(indexVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var vm = new EditUserVm
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = roles.FirstOrDefault(),
                Status = user.Status,
                Roles = _roleManager.Roles.Select(x => new SelectListItem
                {
                    Value = x.Name,
                    Text = x.Name
                }).ToList(),
                Statuses = Enum.GetValues(typeof(UserStatus)).Cast<UserStatus>().Select(s => new SelectListItem
                {
                    Value = ((int)s).ToString(),
                    Text = s.ToString()
                }).ToList()
                
                //JoinDate = user.CreatedAt,

            };

            return Json(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserVm model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList();

                model.Statuses = Enum.GetValues(typeof(UserStatus)).Cast<UserStatus>()
                    .Select(s => new SelectListItem
                    {
                        Value = ((int)s).ToString(),
                        Text = s.ToString()
                    })
                    .ToList();

                return View("Index" ,model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.Status = model.Status;

            var currentRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, model.Role);

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            TempData["Debug"] = "Delete methoduna girildi!";
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) 
            {
                TempData["Error"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Index");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["Succes"] = "Kullanıcı başarıyla silindi";
            }
            else
            {
                TempData["Error"] = "Kullanıcı silinirken bir hata oluştu.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserIndexVm model)
        {
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
                // Rolleri tekrar yükle (form yeniden gösterileceği için lazım)
                model.AddUser.Roles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList();

                model.AddUser.Statuses = Enum.GetValues(typeof(UserStatus)).Cast<UserStatus>().Select(u => new SelectListItem
                {
                    Value = ((int)u).ToString(),
                    Text = u.ToString()
                }).ToList();

                var users = _userManager.Users.ToList();

                var usersVmList = new List<UsersVm>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    usersVmList.Add(new UsersVm
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        Role = roles.FirstOrDefault() ?? "User",
                        JoinDate = user.CreatedAt,
                        Status = user.Status,
                    });
                }

                model.Users = usersVmList;

                return View("Index", model);
            }

            var dto = new AddUserDto
            {
                FullName = model.AddUser.FullName,
                Email = model.AddUser.Email,
                Role = model.AddUser.Role,
                Status = model.AddUser.Status
            };

            // Burada uygulamanın başında inject ettiğimiz UserService kullanılmalı
            var result = await _userService.CreateUserAsync(dto);

            if (!result.Success)
            {
                ModelState.Clear();

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                model.AddUser.Roles = _roleManager.Roles.Select(u => new SelectListItem
                {
                    Value = u.Name,
                    Text = u.Name
                }).ToList();

                model.AddUser.Statuses = Enum.GetValues(typeof(UserStatus)).Cast<UserStatus>().Select(s => new SelectListItem
                {
                    Value = ((int)s).ToString(),
                    Text = s.ToString()
                }).ToList();

                var users = _userManager.Users.ToList();

                var usersVmList = new List<UsersVm>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    usersVmList.Add(new UsersVm
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        Role = roles.FirstOrDefault() ?? "User",
                        JoinDate = user.CreatedAt,
                        Status = user.Status,
                    });
                }

                model.Users = usersVmList;

                return View("Index", model);

            }

            TempData["NewUserEmail"] = dto.Email;
            TempData["NewUserPassword"] = result.Password;

            return RedirectToAction("Index");
        }
    }
}
