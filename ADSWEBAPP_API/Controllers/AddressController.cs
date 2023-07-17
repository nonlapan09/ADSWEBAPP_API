using ADSWEBAPP_API.Data.Address;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADSWEBAPP_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressRepo _addressRepo;
        public AddressController(AddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetProvince()
        {
            try
            {
                var province = await _addressRepo.GetProvinceAsync();
                return Ok(province);
            }
            catch (Exception) { throw; }
        }

        [HttpGet]
        public async Task<ActionResult> GetDistrictWithProvince()
        {
            try
            {
                return Ok();
            }
            catch (Exception) { throw; }
        }

        [HttpGet]
        public async Task<ActionResult> GetSubDistrictWithDistrict()
        {
            try
            {
                return Ok();
            }
            catch (Exception) { throw; }
        }
        [HttpGet]
        public async Task<ActionResult> GetAddressProvinceWithProvinceCode(string provinceCode) 
        {
            try 
            {
                var addressProvince = await _addressRepo.GetAddressProvinceWithProvinceCodeAsync(provinceCode);
                return Ok(addressProvince); 
            } 
            catch (Exception) { throw; }
        }
        [HttpGet]
        public async Task<ActionResult> GetAddressProvinceWithProvinceCodeOptimize(string provinceCode)
        {
            try
            {
                var addressProvince = await _addressRepo.GetAddressProvinceWithProvinceCodeRawQAsync(provinceCode);
                return Ok(addressProvince);
            }
            catch (Exception) { throw; }
        }
    }
}
