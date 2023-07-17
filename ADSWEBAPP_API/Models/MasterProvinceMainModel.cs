using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSWEBAPP_API.Models
{
    [Table("TBL_MASTER_PROVINCE")]
    public class MasterProvinceMainModel
    {
        [Key]
        [Column("TBL_ID")]
        public long Id { get; set; }

        [Column("PROVINCE_CODE")]
        public string ProvinceCode { get; set; } = string.Empty;

        [Column("PROVINCE")]
        public string Province { get; set; } = string.Empty;

        [Column("INACTIVE")]
        public int Inactive { get; set; }

        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }
    }
}
