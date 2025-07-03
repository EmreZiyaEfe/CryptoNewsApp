using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CryptoNewsApp.Web.Areas.Admin.Models.Vms
{
    public class CategoryIndexVm
    {
        public List<CategoryListVm> Categories { get; set; } = new();
        public CreateCategoryVm CreateCategory { get; set; } = new CreateCategoryVm();
        
        [ValidateNever]
        public EditCategoryVm EditCategory { get; set; } = new();
    }
}
