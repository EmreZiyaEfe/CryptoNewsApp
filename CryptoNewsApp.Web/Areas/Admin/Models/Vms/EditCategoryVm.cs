using CryptoNewsApp.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CryptoNewsApp.Web.Areas.Admin.Models.Vms
{
    public class EditCategoryVm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public CategoryStatus Status { get; set; }

        public List<SelectListItem> Statuses { get; set; } = new();
    }

}
