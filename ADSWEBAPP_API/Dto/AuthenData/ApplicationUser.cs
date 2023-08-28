using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSWEBAPP_API.Dto.AuthenData
{
    public class ApplicationUser : IdentityUser
    {
    }

    public class ResponseModel
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class UserRolesModel
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }

    public class Datetime
    {
        public Datetime(DateTime startdate)
        {
            Startdate = startdate;
        }

        public DateTime Startdate { get; }
    }

}
