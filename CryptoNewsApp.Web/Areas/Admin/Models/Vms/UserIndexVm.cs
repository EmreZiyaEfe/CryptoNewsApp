using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CryptoNewsApp.Web.Areas.Admin.Models.Vms
{
    public class UserIndexVm
    {
        public List<UsersVm> Users { get; set; } = new List<UsersVm>();
        public AddUserVm AddUser { get; set; } = new AddUserVm();

        [ValidateNever]
        public EditUserVm EditUser { get; set; } = new EditUserVm();

    }
}
