using ADSWEBAPP_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ADSWEBAPP_API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<MasterAddressMainModel> DbMasterAddress { get; set; } = null!;
        public DbSet<MasterProvinceMainModel> DbMasterProvince { get; set; } = null!;
        public DbSet<MasterDistrictMainModel> DbMasterDistrict { get; set; } = null!;
        public DbSet<MasterSubDistrictMainModel> DbMasterSubDistrict { get; set; } = null!;
    }
}
