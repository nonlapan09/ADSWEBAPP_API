using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ADSWEBAPP_API.Models
{
    [Table("ADS_MASTER_ADDRESS")]
    public class ADS_ADDRESS_MASTER_Model
    {
        [Key]
        [Column("THP_ID")]
        public long ThpId { get; set; }

        [Column("OLD_TBL_ID")]
        public long? DopaOldTblId { get; set; }

        [Column("DOPA_HID")]
        public long DopaHid { get; set; }

        [Column("POI_ID")]
        public long poiid { get; set; }

        [Column("POSTCODE_9")]
        public string? Postcode9 { get; set; }

        [Column("POSTCODE_5_PC9")]
        public string? Postcode5 { get; set; }

        [Column("DB_PC9")]
        public string? DBPC9 { get; set; }

        [Column("DELIVERY_POINT_PC9")]
        public long DeliveryPC9 { get; set; }

        [Column("DOPA_HTYPE_CODE")]
        public string? HTypeCode { get; set; }

        [Column("DOPA_HTYPE_NAME")]
        public string? HTypeName { get; set; }

        [Column("DOPA_HNO")]
        public string? HNO { get; set; }

        [Column("HOUSE_NO_CURRENT")]
        public string? HouseCurrent { get; set; }

        [Column("HOUSE_NO_OLD")]
        public string? HouseOld { get; set; }

        [Column("DOPA_VILLAGE_NO")]
        public string? DopaVillageNo { get; set; }

        [Column("VILLAGE_NO")]
        public long? VillageNo { get; set; }

        [Column("VILLAGE_CODE")]
        public string? VillageCode { get; set; }

        [Column("VILLAGE_NAME")]
        public string? VillageName { get; set; }

        [Column("BUILDING_CODE")]
        public string? BuildingCode { get; set; }

        [Column("BUILDING_NAME")]
        public string? BuildingName { get; set; }

        [Column("BUILDING_FLOOR")]
        public string? BuildingFloor { get; set; }

        [Column("BUILDING_DOOR")]
        public string? BuildingDoor { get; set; }

        [Column("ORG_NAME")]
        public string? OrgName { get; set; }

        [Column("ORG_UNIT")]
        public string? OrgUnit { get; set; }

        [Column("DOPA_RCODE")]
        public string? DopaRCode { get; set; }

        [Column("DOPA_CCAATTMM")]
        public string? DopaCcaattmm { get; set; }

        [Column("LANE_CODE")]
        public string? LaneCode { get; set; }

        [Column("DOPA_LANE_CODE")]
        public string? DopaLaneCode { get; set; }

        [Column("DOPA_LANE_NAME")]
        public string? DopaLaneName { get; set; }

        [Column("ROAD_CODE")]
        public string? RoadCode { get; set; }

        [Column("DOPA_ROAD_CODE")]
        public string? DopaRoadCode { get; set; }

        [Column("DOPA_ROAD_NAME")]
        public string? RoadName { get; set; }

        [Column("ALLEY_CODE")]
        public string? AlleyCode { get; set; }

        [Column("DOPA_ALLEY_CODE")]
        public string? DopaAlleyCode { get; set; }

        [Column("DOPA_ALLEY_NAME")]
        public string? AlleyName { get; set; }

        [Column("DOPA_SUB_DISTRICT_CODE")]
        public string? SubDistrictCode { get; set; }

        [Column("DOPA_SUB_DISTRICT_NAME")]
        public string? SubDistrict { get; set; }

        [Column("DOPA_DISTRICT_NAME")]
        public string? District { get; set; }

        [Column("DOPA_DISTRICT_CODE")]
        public string? DistrictCode { get; set; }

        [Column("DOPA_PROVINCE_CODE")]
        public string? ProvinceCode { get; set; }

        [Column("DOPA_PROVINCE_NAME")]
        public string? Province { get; set; }

        [Column("THP_POSTCODE")]
        public string? Postcode { get; set; }

        [Column("LATITUDE")]
        public long? Latitude { get; set; }

        [Column("LONGITUDE")]
        public long? Longitude { get; set; }

        [Column("PLUSCODE")]
        public string? Pluscode { get; set; }

        [Column("FULLADDRESS")]
        public string? FullAddress { get; set; }

        [Column("DOPA_DATA_AS_OF")]
        public DateTime DataAsOf { get; set; }

        [Column("UPDATE_FLAG")]
        public long? UpdateFlag { get; set; }

        [Column("INACTIVE")]
        public int Inactive { get; set; }

        [Column("CREATED_AT")]
        public DateTime CreateDate { get; set; }

        [Column("UPDATED_AT")]
        public DateTime UpdateDate { get; set; }

        [Column("EXPIRY_DATE")]
        public DateTime ExpiryDate { get; set; }
    }
}
