using CryptoNewsApp.Core.Enums;

namespace CryptoNewsApp.Web.Areas.Admin.Models.Vms
{
    public class CategoryListVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Slug { get; set; }
        public CategoryStatus Status { get; set; }

    }
}
