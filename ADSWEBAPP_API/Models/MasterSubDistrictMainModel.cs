using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSWEBAPP_API.Models
{
    [Table("TBL_MASTER_SUB_DISTRICT")]
    public class MasterSubDistrictMainModel
    {
        [Key]
        [Column("TBL_ID")]
        public long Id { get; set; }

        [Column("SUB_DISTRICT_CODE")]
        public string SubDistrictCode { get; set; } = string.Empty;

        [Column("SUB_DISTRICT")]
        public string SubDistrict { get; set; } = string.Empty;

        [Column("DISTRICT_CODE")]
        public string DistrictCode { get; set; } = string.Empty;

        [Column("PROVINCE_CODE")]
        public string ProvinctCode { get; set; } = string.Empty;

        [Column("INACTIVE")]
        public long Inactive { get; set; }

        [Column("TIMESTAMP_DATE")]
        public DateTime TimeStampDate { get; set; }

    }
}
