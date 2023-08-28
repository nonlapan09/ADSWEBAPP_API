using ADSWEBAPP_API.Data;
using ADSWEBAPP_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static ADSWEBAPP_API.Dto.AuthenData.AuthenticationDbContext;

namespace ADSWEBAPP_API.Dto.AuthenData
{
    public class AuthenticationDbContext : DbContext
    {
    

        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {

        }
        public DbSet<MasterAuthentication> dbMasterAuthentication { get; set; }

    }
}
