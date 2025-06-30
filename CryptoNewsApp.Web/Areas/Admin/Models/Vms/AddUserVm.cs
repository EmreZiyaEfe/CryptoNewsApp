using CryptoNewsApp.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CryptoNewsApp.Web.Areas.Admin.Models.Vms
{
    public class AddUserVm
    {
        //public string Id { get; set; }

        [Required(ErrorMessage = "Full Name alanı zorunludur.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Rol seçilmelidir.")]
        public string Role { get; set; }
        public DateTime JoinDate { get; set; }

        [Required(ErrorMessage = "Durum seçilmelidir.")]
        public UserStatus Status { get; set; }

        public List<SelectListItem> Roles { get; set; } = new();
        public List<SelectListItem> Statuses { get; set; } = new();
    }
}
