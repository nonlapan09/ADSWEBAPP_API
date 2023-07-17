using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSWEBAPP_API.Models
{
    [Table("TBL_MASTER_DISTRICT")]
    public class MasterDistrictMainModel
    {
        [Key]
        [Column("TBL_ID")]
        public long Id { get; set; }

        [Column("DISTRICT_CODE")]
        public string DistrictCode { get; set; } = string.Empty;

        [Column("DISTRICT")]
        public string District { get; set; } = string.Empty;

        [Column("PROVINCE_CODE")]
        public string ProvinceCode { get; set; } = string.Empty;

        [Column("INACTIVE")]
        public long Inactive { get; set; }

        [Column("TIMESTAMP_DATE")]
        public DateTime TimeStampDate { get; set; }
    }
}
