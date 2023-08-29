using ADSWEBAPP_API.Data;
using ADSWEBAPP_API.Data.Address;
using ADSWEBAPP_API.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.VisualBasic;
using System.Text;
using System.Text.Json;

namespace ADSWEBAPP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly AddressRepo _addressRepo;
        private readonly IDistributedCache _cache;
        public AddressController(AddressRepo addressRepo, ILogger<AddressController> logger, IDistributedCache cache)
        {
            _addressRepo = addressRepo;
            _logger = logger;
            _cache = cache;
        }

        [HttpPost]
        [Route("/ads/[controller]/[action]/")]
        [ProducesResponseType(typeof(List<ResponsePostCode>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponsePostCode>> GetMasterCCAATTByPostcode(RequestAddressByPostcode JsonRequest)
        {
            try
            {
                Serilog.Log.Information("Called GetMasterCCAATTByPostcode");
                _logger.LogInformation("API call started...");
                _logger.LogInformation("Requested: GetMasterCCAATTByPostcode | Start | ");
                _logger.LogInformation("Requested: GetMasterCCAATTByPostcode | Process | " + JsonRequest.Postcode);
                _logger.LogInformation("Requested: GetMasterCCAATTByPostcode | Process | " + JsonRequest.Postcode + " | " + JsonSerializer.Serialize(JsonRequest));
               
                
                // Get Client IP
                ///
                //string clientIpAddress = this.httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                /* if (clientIpAddress == JsonRequest.NCAUserDetail[0].IPAddressClient.ToString())
                 {
                     _logger.LogInformation("Requested: GetMasterCCAATTByPostcodeRawQuery | Process Client IP | True | " + clientIpAddress);
     */
                string redisKey = JsonRequest.Postcode.ToString().Trim();

                if (JsonRequest.ClearCache == true)
                {
                    await _cache.RemoveAsync(redisKey);
                }
                var dataFromRedis = await _cache.GetAsync(redisKey);

                List<ResponsePostCode> ResponseAddress = new();

                //if ((dataFromRedis?.Count() ?? 0) > 0)
                if (dataFromRedis != null)
                {

                    var resultStringWithRedis = Encoding.UTF8.GetString(dataFromRedis);

                    ResponseAddress = JsonSerializer.Deserialize<List<ResponsePostCode>>(resultStringWithRedis)!;

                    return Ok(new { LoadedFromRedis = true, Data = ResponseAddress });
                }

                var result = await _addressRepo.GetMasterCCAATTByPostcodeByRawQueryAsync(JsonRequest.Postcode.ToString().Trim());

                if (result?.Count() != 0)
                {
                    /*string cachedDataString = JsonSerializer.Serialize(result);*/
                    var resultDatabaseString = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(result));
                    ResponseAddress = JsonSerializer.Deserialize<List<ResponsePostCode>>(resultDatabaseString)!;
                    

                    // Setting up the cache options
                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                    // Add the data into the cache
                    await _cache.SetAsync(redisKey, resultDatabaseString);
                    return Ok(new { LoadedFromRedis = false, Data = ResponseAddress, options });
                }
                else
                {
                    //return Ok(new { LoadedFromRedis = false, Data = ResponseAddress });
                    return Ok();
                }
            }catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
            /*}
            else
            {
                _logger.LogInformation("Requested: GetMasterCCAATTByPostcodeRawQuery | Process Client IP | False | " + clientIpAddress);
                return Conflict();
            }*/
            //_logger.LogInformation("Requested: GetAddressByPostcode | Process Client IP | " + clientIpAddress);
            ///

        }

        [HttpPost]
        [Route("/ads/[controller]/[action]/")]
        [ProducesResponseType(typeof(List<ResponseDataGetByCCAATT>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseDataGetByCCAATT>> GetRoadLaneAlleyByCCAATT(RequestDataByCCAATT JsonRequest)
        {
            _logger.LogInformation("Requested: GetRoadLaneAlleyByCCAATT | Start | ");
            _logger.LogInformation("Requested: GetRoadLaneAlleyByCCAATT | Process | " + JsonRequest.Province + "|" + JsonRequest.District + "|" + JsonRequest.SubDistrict);
            _logger.LogInformation("Requested: GetRoadLaneAlleyByCCAATT | Process | " + JsonRequest.Province + "|" + JsonRequest.District + "|" + JsonRequest.SubDistrict + " | " + JsonSerializer.Serialize(JsonRequest));

            string redisKey = JsonRequest.Province.ToString().Trim() + JsonRequest.District.ToString().Trim() + JsonRequest.SubDistrict.ToString().Trim();
            if (JsonRequest.ClearCache == true)
            {
                await _cache.RemoveAsync(redisKey);
            }

            var dataFromRedis = await _cache.GetAsync(redisKey);

            List<ResponseDataGetByCCAATT> ResponseDataWithCCAATT = new();

            if (dataFromRedis != null)
            {
                var resultStringWithRedis = Encoding.UTF8.GetString(dataFromRedis);
                ResponseDataWithCCAATT = JsonSerializer.Deserialize<List<ResponseDataGetByCCAATT>>(resultStringWithRedis)!;

                return Ok(new { LoadedFromRedis = true, Data = ResponseDataWithCCAATT });
            }
            var result = await _addressRepo.GetRoadLaneAndAlleyByCCAATT(JsonRequest.Province.ToString().Trim(), JsonRequest.District.ToString().Trim(), JsonRequest.SubDistrict.ToString().Trim());

            if (result?.Count() != 0)
            {
                string cachedDataString = JsonSerializer.Serialize(result);
                var resultDatabaseString = Encoding.UTF8.GetBytes(cachedDataString);
                ResponseDataWithCCAATT = JsonSerializer.Deserialize<List<ResponseDataGetByCCAATT>>(resultDatabaseString)!;

                // Setting up the cache options
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                // Add the data into the cache
                await _cache.SetAsync(redisKey, resultDatabaseString);

                ///await _cache.SetObjectAsync("Postcode:" + redisKey, result);
                return Ok(new { LoadedFromRedis = false, Data = ResponseDataWithCCAATT, options });
            }
            else
            {
                return Ok(new { LoadedFromRedis = false, Data = ResponseDataWithCCAATT });
            }
        }

        [HttpPost]
        [Route("/ads/[controller]/[action]/")]
        [ProducesResponseType(typeof(List<ResponseAddressData>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseAddressData>> GetAddressByPostcodeWithHNO(RequestAddressByPostcodeWithHNO JsonRequest)
        {
            _logger.LogInformation("Requested: GetAddressByPostcodeWithHNO | Start | ");
            _logger.LogInformation("Requested: GetAddressByPostcodeWithHNO | Process | " + JsonRequest.HNO + "|" + JsonRequest.Postcode);
            _logger.LogInformation("Requested: GetAddressByPostcodeWithHNO | Process | " + JsonRequest.HNO + "|" + JsonRequest.Postcode + " | " + JsonSerializer.Serialize(JsonRequest));

            string redisKey = JsonRequest.HNO.ToString().Trim() + JsonRequest.Postcode.ToString().Trim();
            if (JsonRequest.ClearCache == true)
            {
                await _cache.RemoveAsync(redisKey);
            }

            var dataFromRedis = await _cache.GetAsync(redisKey);
            List<ResponseAddressData> ResponseAddress = new();

            if (dataFromRedis != null)
            {
                var resultStringWithRedis = Encoding.UTF8.GetString(dataFromRedis);
                ResponseAddress = JsonSerializer.Deserialize<List<ResponseAddressData>>(resultStringWithRedis)!;
                return Ok(new { LoadedFromRedis = true, Data = ResponseAddress });
            }
            var result = await _addressRepo.GetAddressByPostcodeWithHNOByRawQueryAsync(JsonRequest.HNO.ToString().Trim(), JsonRequest.Postcode.ToString().Trim());

            if (result?.Count() != 0)
            {
                string cachedDataString = JsonSerializer.Serialize(result);
                var resultDatabaseString = Encoding.UTF8.GetBytes(cachedDataString);
                ResponseAddress = JsonSerializer.Deserialize<List<ResponseAddressData>>(resultDatabaseString)!;

                // Setting up the cache options
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                // Add the data into the cache
                await _cache.SetAsync(redisKey, resultDatabaseString);
                return Ok(new { LoadedFromRedis = false, Data = ResponseAddress, options });
            }
            else
            {
                return Ok(new { LoadedFromRedis = false, Data = ResponseAddress });
            }

        }

        [HttpPost]
        [Route("/ads/[controller]/[action]/")]
        [ProducesResponseType(typeof(List<ResponseAddressData>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseAddressData>> GetAddressByPostcodeWithSubDistrict(RequestAddressByPostcodeWithSubDistrict JsonRequest)
        {
            _logger.LogInformation("Requested: GetAddressByPostcodeWithSubDistrict | Start | ");
            _logger.LogInformation("Requested: GetAddressByPostcodeWithSubDistrict | Process | " + JsonRequest.Postcode + "|" + JsonRequest.SubDistrict);
            _logger.LogInformation("Requested: GetAddressByPostcodeWithSubDistrict | Process | " + JsonRequest.Postcode + "|" + JsonRequest.SubDistrict + " | " + JsonSerializer.Serialize(JsonRequest));

            string redisKey = JsonRequest.Postcode.ToString().Trim() + JsonRequest.SubDistrict.ToString().Trim();
            if (JsonRequest.ClearCache == true)
            {
                await _cache.RemoveAsync(redisKey);
            }

            var dataFromRedis = await _cache.GetAsync(redisKey);

            List<ResponseAddressData> ResponseAddressByPostcodeWithCCAATT = new();

            if (dataFromRedis != null)
            {
                var resultStringWithRedis = Encoding.UTF8.GetString(dataFromRedis);
                ResponseAddressByPostcodeWithCCAATT = JsonSerializer.Deserialize<List<ResponseAddressData>>(resultStringWithRedis)!;

                return Ok(new { LoadedFromRedis = true, Data = ResponseAddressByPostcodeWithCCAATT });
            }
            var result = await _addressRepo.GetAddressByPostcodeWithSubDistrictRawQueryAsync(JsonRequest.Postcode, JsonRequest.SubDistrict);


            if (result?.Count() != 0)
            {
                string cachedDataString = JsonSerializer.Serialize(result);
                var resultDatabaseString = Encoding.UTF8.GetBytes(cachedDataString);
                ResponseAddressByPostcodeWithCCAATT = JsonSerializer.Deserialize<List<ResponseAddressData>>(resultDatabaseString)!;

                // Setting up the cache options
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                // Add the data into the cache
                await _cache.SetAsync(redisKey, resultDatabaseString);

                ///await _cache.SetObjectAsync("Postcode:" + redisKey, result);
                return Ok(new { LoadedFromRedis = false, Data = ResponseAddressByPostcodeWithCCAATT, options });
            }
            else
            {
                return Ok(new { LoadedFromRedis = false, Data = ResponseAddressByPostcodeWithCCAATT });
            }
        }

        [HttpPost]
        [Route("/ads/[controller]/[action]/")]
        [ProducesResponseType(typeof(List<ResponseAddressData>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseAddressData>> GetAddressByPostcodeHnoAndVillage(RequestAddressByPostcodeHnoAndVillage JsonRequest)
        {
            _logger.LogInformation("Requested: GetAddressByPostcodeHnoAndVillage | Start | ");
            _logger.LogInformation("Requested: GetAddressByPostcodeHnoAndVillage | Process | " + JsonRequest.Postcode + "|" + JsonRequest.Hno + "|" + JsonRequest.Village);
            _logger.LogInformation("Requested: GetAddressByPostcodeHnoAndVillage | Process | " + JsonRequest.Postcode + "|" + JsonRequest.Hno + "|" + JsonRequest.Village + " | " + JsonSerializer.Serialize(JsonRequest));

            string redisKey = JsonRequest.Postcode.ToString().Trim() + JsonRequest.Hno.ToString().Trim() + JsonRequest.Village.ToString().Trim();
            if (JsonRequest.ClearCache == true)
            {
                await _cache.RemoveAsync(redisKey);
            }

            var dataFromRedis = await _cache.GetAsync(redisKey);

            List<ResponseAddressData> ResponseAddress = new();

            if (dataFromRedis != null)
            {

                var resultStringWithRedis = Encoding.UTF8.GetString(dataFromRedis);

                ResponseAddress = JsonSerializer.Deserialize<List<ResponseAddressData>>(resultStringWithRedis)!;

                return Ok(new { LoadedFromRedis = true, Data = ResponseAddress });
            }
            
            var result = await _addressRepo.GetAddressByPostcodeHnoAndVillageRawQueryAsync(JsonRequest.Postcode.ToString().Trim(), JsonRequest.Hno.ToString().Trim(), JsonRequest.Village.ToString().Trim());

            if (result?.Count() != 0)
            {
                string cachedDataString = JsonSerializer.Serialize(result);
                var resultDatabaseString = Encoding.UTF8.GetBytes(cachedDataString);
                ResponseAddress = JsonSerializer.Deserialize<List<ResponseAddressData>>(resultDatabaseString)!;

                // Setting up the cache options
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                // Add the data into the cache
                await _cache.SetAsync(redisKey, resultDatabaseString);

                ///await _cache.SetObjectAsync("Postcode:" + redisKey, result);
                return Ok(new { LoadedFromRedis = false, Data = ResponseAddress, options });
            }
            else
            {
                return Ok(new { LoadedFromRedis = false, Data = ResponseAddress });
            }
        }

        [HttpPost]
        [Route("/ads/[controller]/[action]/")]
        [ProducesResponseType(typeof(List<ResponseAddressData>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<ResponseAddressData>> GetAddressByPostcodeHNOVillageAndSubdistrict(RequestAddressByPostcodeHNOVillageAndSubdistrict JsonRequest)
        {
            _logger.LogInformation("Requested: GetAddressByPostcodeHNOVillageAndSubdistrict | Start | ");
            _logger.LogInformation("Requested: GetAddressByPostcodeHNOVillageAndSubdistrict | Process | " + JsonRequest.Postcode + " | " + JsonRequest.Hno + " | " + JsonRequest.Village + " | " + JsonRequest.SubDistrict);
            _logger.LogInformation("Requested: GetAddressByPostcodeHNOVillageAndSubdistrict | Process | " + JsonRequest.Postcode + " | " + JsonRequest.Hno + " | " + JsonRequest.Village + " | " + JsonRequest.SubDistrict + " | " + JsonSerializer.Serialize(JsonRequest));


            string redisKey = JsonRequest.Postcode.ToString().Trim() + JsonRequest.Hno.ToString().Trim() + JsonRequest.Village.ToString().Trim() + JsonRequest.SubDistrict.ToString().Trim();

            if (JsonRequest.ClearCache == true)
            {
                await _cache.RemoveAsync(redisKey);
            }

            var dataFromRedis = await _cache.GetAsync(redisKey);

            List<ResponseAddressData> ResponseAddress = new();

            //if ((dataFromRedis?.Count() ?? 0) > 0)
            if (dataFromRedis != null)
            {

                var resultStringWithRedis = Encoding.UTF8.GetString(dataFromRedis);

                ResponseAddress = JsonSerializer.Deserialize<List<ResponseAddressData>>(resultStringWithRedis)!;

                return Ok(new { LoadedFromRedis = true, Data = ResponseAddress });
            }

            var result = await _addressRepo.GetAddressByPostcodeHNOVillageAndSubdistrictRawQueryAsync(JsonRequest.Postcode.ToString().Trim(), JsonRequest.Hno.ToString().Trim(), JsonRequest.Village.ToString().Trim(), JsonRequest.SubDistrict.ToString().Trim());

            if (result?.Count() != 0)
            {
                /*string cachedDataString = JsonSerializer.Serialize(result);*/
                var resultDatabaseString = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(result));
                ResponseAddress = JsonSerializer.Deserialize<List<ResponseAddressData>>(resultDatabaseString)!;

                // Setting up the cache options
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                // Add the data into the cache
                await _cache.SetAsync(redisKey, resultDatabaseString);

                return Ok(new { LoadedFromRedis = false, Data = ResponseAddress, options });
            }
            else
            {
                return Ok(new { LoadedFromRedis = false, Data = ResponseAddress });
                
            }

        }

    }
}
