using CryptoNewsApp.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CryptoNewsApp.Web.Areas.Admin.Models.Vms
{
    public class UsersVm
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime JoinDate { get; set; }
        public UserStatus Status { get; set; }

    }
}
