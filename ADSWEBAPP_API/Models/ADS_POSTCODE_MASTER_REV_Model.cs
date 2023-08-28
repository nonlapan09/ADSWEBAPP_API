using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSWEBAPP_API.Models
{
    [Keyless]
    [Table("ADS_POSTCODE_MASTER_REV")]
    public class ADS_POSTCODE_MASTER_REV_Model
    {
        
        [Column("THP_POSTCODE")]
        public string ThpPostcode { get; set; } = string.Empty;

        [Column("DOPA_PROVINCE_CODE")]
        public string DopaProviceCode { get; set; } = string.Empty;

        [Column("DOPA_PROVINCE_NAME")]
        public string DopaProviceName { get; set; } = string.Empty;

        [Column("DOPA_DISTRICT_CODE")]
        public string DopaDistrictCode { get; set; } = string.Empty;

        [Column("DOPA_DISTRICT_NAME")]
        public string DopaDistrictName { get; set; } = string.Empty;

        [Column("DOPA_SUB_DISTRICT_CODE")]
        public string DopaSubDistrictCode { get; set; } = string.Empty;

        [Column("DOPA_SUB_DISTRICT_NAME")]
        public string DopaSubDistrictName { get; set; } = string.Empty;


    }
}
