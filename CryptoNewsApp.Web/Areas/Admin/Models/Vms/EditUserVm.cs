using CryptoNewsApp.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CryptoNewsApp.Web.Areas.Admin.Models.Vms
{
    public class EditUserVm
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage ="FullName zorunludur.")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public UserStatus Status { get; set; }

        public List<SelectListItem> Roles { get; set; } = new();
        public List<SelectListItem> Statuses { get; set; } = new();
    }
}
