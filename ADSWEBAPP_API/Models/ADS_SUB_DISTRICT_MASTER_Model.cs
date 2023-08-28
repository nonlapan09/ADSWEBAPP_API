using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSWEBAPP_API.Models
{
    [Table("ADS_SUB_DISTRICT_MASTER")]
    public class ADS_SUB_DISTRICT_MASTER_Model
    {
        [Key]
        [Column("SUB_DISTRICT_ID")]
        public long Id { get; set; }

        [Column("LANG_ID")]
        public int? LangId { get; set; }

        [Column("SUB_DISTRICT_CODE")]
        public int SubDistrictCode { get; set; }

        [Column("SUB_DISTRICT_FULL_NAME")]
        public string SubDistrictFullName { get; set; } = string.Empty;

        [Column("SUB_DISTRICT_NAME")]
        public string SubDistrictName { get; set; } = string.Empty;

        [Column("DISTRICT_CODE")]
        public int DistrictCode { get; set; }

        [Column("DISTRICT_NAME")]
        public string DistrictName { get; set; } = string.Empty;

        [Column("PROVINCE_CODE")]
        public int? ProvinceCode { get; set; }

        [Column("PROVINCE_NAME")]
        public string ProvinceName { get; set; } = string.Empty;

        [Column("DOPA_DATA_AS_OF")]
        public DateTime? DopaDataAsOf { get; set; }

        [Column("UPDATE_FLAG")]
        public int? UpdateFlag { get; set; }

        [Column("INACTIVE")]
        public int Inactive { get; set; }

        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }

        [Column("UPDATED_AT")]
        public DateTime? UpdatedAt { get; set; }

        [Column("EXPIRY_DATE")]
        public DateTime? ExpiryDate { get; set; }
    }
}
