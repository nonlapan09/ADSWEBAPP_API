using ADSWEBAPP_API.Models;

namespace ADSWEBAPP_API.Dto
{
    public class AddressProvinceResponse
    {
        public int Total { get; set; }
        public List<MasterAddressMainModel>? Data {  get; set; }
    }
}
