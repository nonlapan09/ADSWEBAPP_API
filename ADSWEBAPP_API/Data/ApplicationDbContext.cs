using ADSWEBAPP_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ADSWEBAPP_API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<ADS_PROVINCE_MASTER_Model> DbMasterProvince { get; set; } = null!;
        public DbSet<ADS_DISTRICT_MASTER_Model> DbMasterDistrict { get; set; } = null!;
        public DbSet<ADS_SUB_DISTRICT_MASTER_Model> DbMasterSubDistrict { get; set; } = null!;
        public DbSet<ADS_ROAD_MASTER_Model> DbMasterRoad { get; set; } = null!;
        public DbSet<ADS_LANE_MASTER_Model> ADSMasterLane { get; set; }
        public DbSet<ADS_ALLEY_MASTER_Model> ADSMastersAlley { get; set; }
        public DbSet<ADS_POSTCODE_MASTER_REV_Model> DbMasterPostcode { get; set; } = null!;

        public DbSet<ADS_ADDRESS_MASTER_Model> DbAddress { get; set; } = null!;
        //public DbSet<ADS_LOGGING_Model> DbLogAPI { get; set; } = null!;

    }
}
