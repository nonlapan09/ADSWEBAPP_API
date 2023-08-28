using ADSWEBAPP_API.Data.Address;
using ADSWEBAPP_API.Dto;
using ADSWEBAPP_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADSWEBAPP_API.Controllers
{
    [Route("ads/master/[action]")]
    [ApiController]
    [Authorize]
    public class MasterController : ControllerBase
    {
        private readonly AddressRepo _addressRepo;
        private readonly ILogger<MasterController> _logger;
        public MasterController(AddressRepo addressRepo, ILogger<MasterController> logger)
        {
            _addressRepo = addressRepo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ProvinceData>?> GetProvinceAll(int langid)
        {
            try
            {
                _logger.LogInformation("Requested: GetProvinceAll | Process | " + langid);
                var province = await _addressRepo.GetProvinceAllAsync(langid);
                return Ok(province);
            }
            catch (Exception) { throw; }
        }

        [HttpGet]
        public async Task<ActionResult<DistrictData>?> GetDistrictWithProvince(int provinceCode , int langid)
        {
            try
            {
                _logger.LogInformation("Requested: GetDistrictWithProvince | Process | " + provinceCode + " | " + langid);
                var district = await _addressRepo.GetDistricttWithProvinceAsync(provinceCode ,  langid);
                return Ok(district);
            }
            catch (Exception) { throw; }
        }

        [HttpGet]
        public async Task<ActionResult<SubDistrictData>?> GetSubDistrictWithDistrict(int districtCode, int langid)
        {
            try
            {
                _logger.LogInformation("Requested: GetSubDistrictWithDistrict | Process | " + districtCode + " | " + langid);
                var subdistrict = await _addressRepo.GetSubDistrictWithDistrictAsync(districtCode , langid);
                return Ok(subdistrict);
            }
            catch (Exception) { throw; }
        }


        [HttpGet]
        public async Task<ActionResult<RoadData>?> GetRoadWithProvince(int provincecode, int langid)
        {
            try
            {
                _logger.LogInformation("Requested: GetRoadWithProvince | Process | " + provincecode + " | " + langid);
                var Road = await _addressRepo.GetRoadWithProvinceAsync(provincecode, langid);
                return Ok(Road);
            }
            catch (Exception) { throw; }
        }


        [HttpGet]
        public async Task<ActionResult<LaneData>?> GetLaneWithRCode(string rcode, int langid)
        {
            try
            {
                _logger.LogInformation("Requested: GetLaneWithRCode | Process | " + rcode + " | " + langid);

                var Lane = await _addressRepo.GetLaneWithRCodeAsync(rcode, langid);
                return Ok(Lane);
            }
            catch (Exception) { throw; }
        }


        [HttpGet]
        public async Task<ActionResult<RoadData>?> GetAlleyWithRCode(string rcode, int langid)
        {
            try
            {
                _logger.LogInformation("Requested: GetAlleyWithRCode | Process | " + rcode + " | " + langid);
                var Alley = await _addressRepo.GetAlleyWithRCodeAsync(rcode, langid);
                return Ok(Alley);
            }
            catch (Exception) { throw; }
        }


        [HttpGet]
        public async Task<ActionResult<PostcodeData>?> GetPostcodeBySubDistrict(string subdistrictcode)
        {
            try
            {
                _logger.LogInformation("Requested: GetPostcodeBySubDistrict | Process | " + subdistrictcode);
                var postcode = await _addressRepo.GetPostcodeBySubDistrictAsync(subdistrictcode);
                return Ok(postcode);
            }
            catch (Exception) { throw; }
        }



        [HttpGet]
        public async Task<ActionResult<PostcodeData>?> GetCCTTAAByPostcode(string postcode)
        {
            try
            {
                _logger.LogInformation("Requested: GetCCTTAAByPostcode | Process | " + postcode);
                var CCTTAA = await _addressRepo.GetCCTTAAByPostcodeAsync(postcode);
                return Ok(CCTTAA);
            }
            catch (Exception) { throw; }
        }

  

    }
}
