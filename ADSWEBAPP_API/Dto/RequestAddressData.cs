using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ADSWEBAPP_API.Dto
{
    public class RequestAddressData
    {
        
    }
    public class RequestAddressByPostcode
    {

        [Required]
        [MaxLength(5), MinLength(5)]
        public string Postcode { get; set; } = string.Empty;

        [Required]
        [DefaultValue(false)]
        public Boolean? ClearCache { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }


    public class RequestDataByCCAATT
    {
        [Required(ErrorMessage = "Province is required")]
        public string Province { get; set; } = string.Empty;

        [Required(ErrorMessage = "District is required")]
        public string District { get; set; } = string.Empty;

        [Required(ErrorMessage = "SubDistrict is required")]
        public string SubDistrict { get; set; } = string.Empty;
        [Required]
        [DefaultValue(false)]
        public Boolean? ClearCache { get; set; }
    }

    public class RequestAddressByPostcodeWithHNO
    {
        [Required(ErrorMessage = "HNO is required")]
        public string HNO { get; set; } = string.Empty;

        [Required(ErrorMessage = "Postcode is required")]
        [MaxLength(5), MinLength(5)]
        public string Postcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "ClearCache is required")]
        [DefaultValue(false)]
        public Boolean? ClearCache { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public class RequestAddressByPostcodeWithSubDistrict
    {
        [Required]
        [MaxLength(5), MinLength(5)]
        public string Postcode { get; set; } = string.Empty;

        [Required]
        public string SubDistrict { get; set; } = string.Empty;
        //public string Hno { get; set; }
        [Required]
        [DefaultValue(false)]
        public Boolean ClearCache { get; set; }
    }

    public class RequestAddressByPostcodeHnoAndVillage
    {
        [Required]
        [MaxLength(5), MinLength(5)]
        public string Postcode { get; set; } = string.Empty;
        [Required]
        public string Hno { get; set; } = string.Empty;
        
        public string Village { get; set; } = string.Empty;
        [Required]
        [DefaultValue(false)]
        public Boolean? ClearCache { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public class RequestAddressByPostcodeHNOVillageAndSubdistrict
    {
        [Required]
        [MaxLength(5), MinLength(5)]
        public string Postcode { get; set; } = string.Empty;
        [Required]
        public string Hno { get; set; } = string.Empty;

        public string Village { get; set; } = string.Empty;

        [Required]
        public string SubDistrict { get; set; } = string.Empty;

        [Required]
        [DefaultValue(false)]
        public Boolean ClearCache { get; set; }
    }
}
