using ADSWEBAPP_API.Models;
using static ADSWEBAPP_API.Data.Address.AddressRepo;

namespace ADSWEBAPP_API.Dto
{
    

    public class AddressProvinceCountResponse
    {
        public int Total { get; set; }
        public List<ProvinceData>? Data { get; set; }
    }
    
    public class ProvinceData
    {
        public string Province { get; set; } = string.Empty;
        public int? ProvinceCode { get; set; } 
        public string ProvinceAbbr { get; set; } = string.Empty;
        public int? PostcodeProvincePrefix { get; set; }
        public int? ThpRegionId { get; set; }
        public string Postcode { get; set; } = string.Empty;
        public string RegionName4 { get; set; } = string.Empty;
        public string RegionName6 { get; set; } = string.Empty;
        public string RegionTat { get; set; } = string.Empty;
        public string BangkokVicinity { get; set; } = string.Empty;

    }

    public class DistrictData
    {
        public string District { get; set; } = string.Empty;
        public int? DistrictCode { get; set; }
        public string Province { get; set; } = string.Empty;
        public int? ProvinceCode { get; set; }
        
    }



    public class SubDistrictData
    {
        public string SubDistrict { get; set; } = string.Empty;
        public int? SubDistrictCode { get; set; }
        public string District { get; set; } = string.Empty;
        public int? DistrictCode { get; set; } 
        public string Province { get; set; } = string.Empty;
        public int? ProvinceCode { get; set; }
    }


  


    public class DistrictCountResponse
    {
        public int Total { get; set; }
        public List<DistrictData>? DistrictData { get; set; }
    }





    public class RoadData
    {
        
        public string Road { get; set; } = string.Empty;
        public string RCode { get; set; } = string.Empty;
        public string RoadCode { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public int? ProvinceCode { get; set; }
    }

    

   


    public class LaneData
    {

        public string LaneCurrent { get; set; } = string.Empty;
        public string LaneOld { get; set; } = string.Empty;

        public string LaneCode { get; set; } = string.Empty;
        public string RCode { get; set; } = string.Empty;

    }


    public class AlleyData
    {

        public string Alley { get; set; } = string.Empty;
        public int? AlleyCode { get; set; }
        public string RCode { get; set; } = string.Empty;
    }

     public class PostcodeData
     {
      public string Postcode { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string ProvinceCode { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string DistrictCode { get; set; } = string.Empty;
    public string SubDistrict { get; set; } = string.Empty;
    public string SubDistrictCode { get; set; } = string.Empty;

        }

}