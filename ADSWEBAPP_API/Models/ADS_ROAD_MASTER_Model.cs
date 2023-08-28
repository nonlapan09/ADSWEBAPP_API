using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSWEBAPP_API.Models
{
    [Table("ADS_ROAD_MASTER")]
    public class ADS_ROAD_MASTER_Model
    {
        [Key]
        [Column("ROAD_ID")]
        public long Id { get; set; }

        [Column("LANG_ID")]
        public int? LangId { get; set; }

        [Column("ROAD_CODE")]
        public string RoadCode { get; set; } = string.Empty;

        [Column("ROAD_NAME")]
        public string RoadName { get; set; } = string.Empty;

        [Column("RCODE")]
        public string RCode { get; set; } = string.Empty;

        [Column("THP_POSTCODE")]
        public int? ThpPostcode { get; set; }

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
