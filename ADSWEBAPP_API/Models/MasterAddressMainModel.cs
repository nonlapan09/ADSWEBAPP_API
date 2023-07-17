using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ADSWEBAPP_API.Models
{
    [Table("TBL_MASTER_ADDRESS")]
    public class MasterAddressMainModel
    {
        [Key]
        [Column("TBL_ID")]
        public long ThpAdsId { get; set; }

        [Column("DOPA_HID_NO")]
        public long DopaHid { get; set; }

        [Column("DOPA_HNO")]
        public string? HNO { get; set; } = string.Empty;

        [Column("VILLAGE")]
        public string? Village { get; set; } = string.Empty;

        [Column("VILLAGE_NAME")]
        public string? VillageName { get; set; } = string.Empty;

        [Column("DOPA_LANE")]
        public string? Lane { get; set; } = string.Empty;

        [Column("DOPA_ROAD")]
        public string? Road { get; set; } = string.Empty;

        [Column("DOPA_ALLEY")]
        public string? Alley { get; set; } = string.Empty;
        [Column("DOPA_SUB_DISTRICT_CODE")]
        public string SubDistrictCode { get; set; } = string.Empty;

        [Column("DOPA_SUB_DISTRICT")]
        public string SubDistrict { get; set; } = string.Empty;

        [Column("DOPA_DISTRICT_CODE")]
        public string DistrictCode { get; set; } = string.Empty;

        [Column("DOPA_DISTRICT")]
        public string District { get; set; } = string.Empty;

        [Column("DOPA_PROVINCE_CODE")]
        public string ProvinceCode { get; set; } = string.Empty;

        [Column("DOPA_PROVINCE")]
        public string Province { get; set; } = string.Empty;

        [Column("DOPA_RCDE_CODE")]
        public string? RCode { get; set; } = string.Empty;

        [Column("DOPA_CCAATTMM_CODE")]
        public string? ccaattmmCode { get; set; } = string.Empty;

        //[Column("DOPA_HNO")]
        //public string HNO { get; set; }

        //[Column("DOPA_HNO")]
        //public string HNO { get; set; }

        [Column("THP_POSTCODE")]
        public string? Postcode { get; set; } = string.Empty;

        [Column("INACTIVE")]
        public int Inactive { get; set; }
        

    }
}
