using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSWEBAPP_API.Models
{
    [Table("ADS_PROVINCE_MASTER")]
    public class ADS_PROVINCE_MASTER_Model
    {
        [Key]
        [Column("PROVINCE_ID")]
        public long Id { get; set; }

        [Column("LANG_ID")]
        public int? LangId { get; set; }

        [Column("PROVINCE_CODE")]
        public int? ProvinceCode { get; set; }

        [Column("PROVINCE_NAME")]
        public string ProvinceName { get; set; } = string.Empty;

        [Column("PROVINCE_ABBR")]
        public string ProvinceAbbr { get; set; } = string.Empty;

        [Column("POSTCODE_PROVINCE_PREFIX")]
        public int? PostcodeProvincePrefix { get; set; }

        [Column("THP_REGION_ID")]
        public int? ThpRegionId { get; set; }

        [Column("POSTCODE")]
        public string Postcode { get; set; } = string.Empty;

        [Column("REGION_NAME_4")]
        public string RegionName4 { get; set; } = string.Empty;

        [Column("REGION_NAME_6")]
        public string RegionName6 { get; set; } = string.Empty;

        [Column("REGION_TAT")]
        public string RegionTat { get; set; } = string.Empty;

        [Column("BANGKOK_VICINITY")]
        public string BangkokVicinity { get; set; } = string.Empty;

        [Column("DOPA_DATA_AS_OF")]
        public DateTime? DopaDataAsOf { get; set; }

        [Column("UPDATE_FLAG")]
        public int? UpdateFlag { get; set; }

        [Column("INACTIVE")]
        public int? Inactive { get; set; }

        [Column("CREATED_AT")]
        public DateTime? CreatedAt { get; set; }

        [Column("UPDATED_AT")]
        public DateTime? UpdatedAt { get; set; }

        [Column("EXPIRY_DATE")]
        public DateTime? ExpiryDate { get; set; }

    }
}
