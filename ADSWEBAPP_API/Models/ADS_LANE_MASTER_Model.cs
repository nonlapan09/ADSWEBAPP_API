using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSWEBAPP_API.Models
{
    [Keyless]
    [Table("ADS_LANE_MASTER")]
    public class ADS_LANE_MASTER_Model
    {
        
        [Column("LANE_ID")]
        public int? LaneId { get; set; }

        [Column("LANG_ID")]
        public int? LangId { get; set; }

        [Column("LANE_CODE")]
        public string LaneCode { get; set; } = string.Empty;

        [Column("LANE_NAME_CURRENT")]
        public string LaneNameCurrent { get; set; } = string.Empty;

        [Column("LANE_NAME_OLD")]
        public string LaneNameOld { get; set; } = string.Empty;

        [Column("RCODE")]
        public string RCode { get; set; } = string.Empty;

        [Column("THP_POSTCODE")]
        public string ThpPostcode { get; set; } = string.Empty;

        [Column("DOPA_DATA_AS_OF")]
        public DateTime DopaDataAsOf { get; set; }

        [Column("UPDATE_FLAG")]
        public int? UpdateFlag { get; set; }

        [Column("INACTIVE")]
        public int? Inactive { get; set; }

        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }

        [Column("UPDATED_AT")]
        public DateTime UpdatedAt { get; set; }

        [Column("EXPIRY_DATE")]
        public DateTime ExpiryDate { get; set; }
    }
}