using ADSWEBAPP_API.Dto;
using ADSWEBAPP_API.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using StackExchange.Redis;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.ComponentModel.Design.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Drawing.Drawing2D;

namespace ADSWEBAPP_API.Data.Address
{
    public class AddressRepo
    {
        private readonly ApplicationDbContext _context;
        public AddressRepo(ApplicationDbContext context) 
        {
            _context = context;
        }


        public async Task<List<ProvinceData>?> GetProvinceAllAsync(int? langId = null)
        {
            try
            {
                var provinceQuery = _context.DbMasterProvince
                    .AsNoTracking()
                    .Where(p => p.Inactive != 1 && p.ExpiryDate == null);

                if (langId.HasValue && (langId == 1 || langId == 2))
                {
                    provinceQuery = provinceQuery.Where(p => p.LangId == langId);
                }
                else if (langId.HasValue && langId != 0)
                {
                    throw new ArgumentException("Invalid langId. Only 1(ภาษาไทย) and 2(ภาษาอังกฤษ) are allowed.");
                }

                var provinceData = await provinceQuery
                    .OrderBy(p => p.ProvinceCode)
                    .ThenBy(p => p.LangId)
                    .Select(p => new ProvinceData
                    {
                        Province = p.ProvinceName,
                        ProvinceCode = p.ProvinceCode,
                        ProvinceAbbr = p.ProvinceAbbr,
                        PostcodeProvincePrefix = p.PostcodeProvincePrefix,
                        ThpRegionId = p.ThpRegionId,
                        Postcode = p.Postcode,
                        RegionName4 = p.RegionName4,
                        RegionName6 = p.RegionName6,
                        RegionTat = p.RegionTat,
                        BangkokVicinity = p.BangkokVicinity
                    })
                    .ToListAsync();

                return provinceData;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<DistrictData>?> GetDistricttWithProvinceAsync(int provinceCode, int? langId = null)
        {
            try
            {
                var districtQuery = _context.DbMasterDistrict
                    .AsNoTracking()
                    .Where(d => d.Inactive != 1 && d.ExpiryDate == null && d.ProvinceCode == provinceCode);

                if (langId.HasValue && (langId == 1 || langId == 2))
                {
                    districtQuery = districtQuery.Where(d => d.LangId == langId);
                }
                else if (langId.HasValue && langId != 0)
                {
                    throw new ArgumentException("Invalid langId. Only 1(ภาษาไทย) and 2(ภาษาอังกฤษ) are allowed.");
                }

                var districtData = await districtQuery
                    .Select(d => new DistrictData
                    {
                        District = d.DistrictName,
                        DistrictCode = d.DistrictCode,
                        Province = d.ProvinceName,
                        ProvinceCode = d.ProvinceCode,
                    })
                    .OrderBy(d => d.DistrictCode)
                    .ToListAsync();

                return districtData;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<SubDistrictData>?> GetSubDistrictWithDistrictAsync(int districtCode, int? langId = null)
        {
            try
            {
                var subDistrictQuery = _context.DbMasterSubDistrict
                    .AsNoTracking()
                    .Where(sd => sd.Inactive != 1 && sd.ExpiryDate == null && sd.DistrictCode == districtCode);

                if (langId.HasValue && (langId == 1 || langId == 2))
                {
                    subDistrictQuery = subDistrictQuery.Where(sd => sd.LangId == langId);
                }
                else if (langId.HasValue && langId != 0)
                {
                    throw new ArgumentException("Invalid langId. Only 1(ภาษาไทย) and 2(ภาษาอังกฤษ) are allowed.");
                }

                var subDistrictData = await subDistrictQuery
                    .OrderBy(sd => sd.SubDistrictCode)
                    .ThenBy(sd => sd.LangId)
                    .Select(sd => new SubDistrictData
                    {
                        SubDistrict = sd.SubDistrictName,
                        SubDistrictCode = sd.SubDistrictCode,
                        District = sd.DistrictName,
                        DistrictCode = sd.DistrictCode,
                        Province = sd.ProvinceName,
                        ProvinceCode = sd.ProvinceCode
                    })
                    .ToListAsync();

                return subDistrictData;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<RoadData>?> GetRoadWithProvinceAsync(int? provincecode, int? langId = null)
        {
            try
            {
                
                //Task<List<RoadData>?>
                var provincecCodeStr = provincecode.ToString();

                var roadDataQuery = _context.DbMasterRoad
                    .AsNoTracking()
                    .Where(w => w.RCode.Substring(0, 2) == provincecCodeStr && w.Inactive != 1 && w.ExpiryDate == null);

                var ProvinceDataQuery = _context.DbMasterProvince.AsNoTracking();

                if (langId.HasValue && (langId == 1 || langId == 2))
                {
                    roadDataQuery = roadDataQuery.Where(rd => rd.LangId == langId);
                    ProvinceDataQuery = ProvinceDataQuery.Where(sd => sd.LangId == langId);
                }
                else if (langId.HasValue && langId != 0)
                {
                    throw new ArgumentException("Invalid langId. Only 1(ภาษาไทย) and 2(ภาษาอังกฤษ) are allowed.");
                }

                var roadData = await roadDataQuery.ToListAsync();
                var ProvinceData = await ProvinceDataQuery.ToListAsync();

                
                var joinResult = roadData.Join(
                    ProvinceData,
                    t1 => t1.RCode.Substring(0,2),
                    t2 => t2.ProvinceCode.ToString(),
                    (t1,t2) => new
                    {
                        ProvinceCode = t2.ProvinceCode,
                        Province = t2.ProvinceName,
                        Road = t1.RoadName,
                        RCode = t1.RCode,
                        RoadCode = t1.RoadCode,
                        langIdP = t2.LangId,
                        langIdR = t1.LangId

                    })
                    .Where(w=>w.langIdR == w.langIdP)
                    .Select(s => new RoadData { ProvinceCode = s.ProvinceCode, Province = s.Province, Road = s.Road, RCode = s.RCode,RoadCode =  s.RoadCode})
                    .OrderBy(r=>r.RoadCode)
                    .ToList();

                return joinResult;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<LaneData>?> GetLaneWithRCodeAsync(string rcode, int? langId = null)
        {
            try
            {
                var laneQuery = _context.ADSMasterLane
                    .AsNoTracking()
                    .Where(lane => lane.RCode == rcode);

                if (langId.HasValue && (langId == 1 || langId == 2))
                {
                    laneQuery = laneQuery.Where(lane => lane.LangId == langId);
                }
                else if (langId.HasValue && langId != 0)
                {
                    throw new ArgumentException("Invalid langId. Only 1(ภาษาไทย) and 2(ภาษาอังกฤษ) are allowed.");
                }

                var laneData = await laneQuery
                    .OrderBy(lane => lane.LaneCode)
                    .ThenBy(lane => lane.LangId)
                    .Select(lane => new LaneData
                    {
                        LaneCurrent = lane.LaneNameCurrent,
                        LaneOld = lane.LaneNameOld ?? "",
                        LaneCode = lane.LaneCode,
                        RCode = lane.RCode
                    })
                    .ToListAsync();

                if (laneData.Count == 0)
                {
                    throw new Exception("No data found for the provided RCode.");
                }

                return laneData;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<AlleyData>?> GetAlleyWithRCodeAsync(string rcode, int? langId = null)
        {
            try
            {
                var alleyQuery = _context.ADSMastersAlley
                    .AsNoTracking()
                    .Where(alley => alley.RCode == rcode);

                if (langId.HasValue && (langId == 1 || langId == 2))
                {
                    alleyQuery = alleyQuery.Where(alley => alley.LangId == langId);
                }
                else if (langId.HasValue && langId != 0)
                {
                    throw new ArgumentException("Invalid langId. Only 1(ภาษาไทย) and 2(ภาษาอังกฤษ) are allowed.");
                }

                var alleyData = await alleyQuery
                    .OrderBy(alley => alley.AlleyCode)
                    .ThenBy(alley => alley.LangId)
                    .Select(alley => new AlleyData
                    {
                        Alley = alley.AlleyName,
                        AlleyCode = alley.AlleyCode,
                        RCode = alley.RCode
                    })
                    .ToListAsync();

                if (alleyData.Count == 0)
                {
                    throw new Exception("No data found for the provided RCode.");
                }

                return alleyData;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<PostcodeData>?> GetPostcodeBySubDistrictAsync(string subdistrictcode)
        {
            try
            {
                var postcodeData = await _context.DbMasterPostcode
                    .AsNoTracking()
                    .Where(p => p.DopaSubDistrictCode == subdistrictcode)
                    .OrderBy(p => p.DopaSubDistrictCode)
                    .ToListAsync();

                var result = postcodeData
                    .Select(p => new PostcodeData
                    {
                        Postcode = p.ThpPostcode,
                        Province = p.DopaProviceName,
                        ProvinceCode = p.DopaProviceCode,
                        District = p.DopaDistrictName,
                        DistrictCode = p.DopaDistrictCode,
                        SubDistrict = p.DopaSubDistrictName,
                        SubDistrictCode = p.DopaSubDistrictCode
                    })
                    .ToList();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<PostcodeData>?> GetCCTTAAByPostcodeAsync(string postcode)
        {
            try
            {
                // ค้นหาข้อมูลตามรหัสไปรษณีย์ในตาราง DbMasterPostcode
                var postcodeQuery = await _context.DbMasterPostcode
                    .AsNoTracking()
                    .Where(p => p.ThpPostcode == postcode)
                    .OrderBy(p => p.DopaSubDistrictCode)
                    .ToListAsync();

                // แปลงผลลัพธ์การค้นหาเป็น PostcodeData
                var postcodeData = postcodeQuery
                    .Select(p => new PostcodeData
                    {
                        Postcode = p.ThpPostcode,
                        Province = p.DopaProviceName,
                        ProvinceCode = p.DopaProviceCode,
                        District = p.DopaDistrictName,
                        DistrictCode = p.DopaDistrictCode,
                        SubDistrict = p.DopaSubDistrictName,
                        SubDistrictCode = p.DopaSubDistrictCode
                    })
                    .ToList();

                // คืนค่ารายการข้อมูลที่แปลงแล้ว
                return postcodeData;
            }
            catch
            {
                throw;
            }
        }


        public async Task<IEnumerable<ResponsePostCode>?> GetMasterCCAATTByPostcodeByRawQueryAsync(string sPostcode)
        {
            var connection = _context.Database.GetDbConnection();
            try
            {
                List<ResponsePostCode> rolesAddress = new List<ResponsePostCode>();
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    string query = "SELECT DOPA_SUB_DISTRICT_NAME, DOPA_DISTRICT_NAME,DOPA_PROVINCE_NAME,THP_POSTCODE FROM ADS_ADDRESS_MASTER " +
                        "WHERE THP_POSTCODE = " +
                        " '" + sPostcode + "'" +
                        "AND EXPIRY_DATE IS NULL GROUP BY DOPA_SUB_DISTRICT_NAME, DOPA_DISTRICT_NAME,DOPA_PROVINCE_NAME,THP_POSTCODE " +
                        "ORDER BY DOPA_DISTRICT_NAME,DOPA_SUB_DISTRICT_NAME ASC";

                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using (var obj = await command.ExecuteReaderAsync())
                    {
                        long i = 1;

                        if (obj.HasRows)
                        {
                            while (await obj.ReadAsync())
                            {
                                var resPostCode = new ResponsePostCode()
                                {
                                    Seq = (long)i++,
                                    Province = obj.GetValue(2).ToString() ?? "",
                                    District = obj.GetValue(1).ToString() ?? "",
                                    SubDistrict = obj.GetValue(0).ToString() ?? ""
                                };
                                rolesAddress.Add(resPostCode);
                            }
                        }
                    }
                }
                await connection.CloseAsync();
                return rolesAddress;

            }
            catch (Exception ex)
            {
                await connection.CloseAsync();
                Console.WriteLine("ERROR:", ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<ResponseDataGetByCCAATT>?> GetRoadLaneAndAlleyByCCAATT(string sProvince, string sDistrict, string sSubDistrict)
        {
            var connection = _context.Database.GetDbConnection();
            try
            {
                List<ResponseDataGetByCCAATT> roles = new List<ResponseDataGetByCCAATT>();
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    string query = "SELECT DOPA_HNO, VILLAGE_NO, DOPA_ROAD_NAME, DOPA_LANE_NAME, DOPA_ALLEY_NAME FROM ADS_ADDRESS_MASTER " +
                        "WHERE DOPA_PROVINCE_NAME LIKE " + " '%" + sProvince + "%'" +
                        "AND DOPA_DISTRICT_NAME LIKE " + " '%" + sDistrict + "%'" +
                        "AND DOPA_SUB_DISTRICT_NAME LIKE " + " '%" + sSubDistrict + "%'" +
                        " AND INACTIVE = 0 AND EXPIRY_DATE IS NULL" +
                        " GROUP BY DOPA_HNO, VILLAGE_NO, DOPA_ROAD_NAME, DOPA_LANE_NAME,DOPA_ALLEY_NAME,THP_POSTCODE, DOPA_PROVINCE_CODE " +
                        "ORDER BY THP_POSTCODE, DOPA_PROVINCE_CODE";

                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using (var obj = await command.ExecuteReaderAsync())
                    {
                        long i = 1;

                        if (obj.HasRows)
                        {
                            while (await obj.ReadAsync())
                            {
                                var res = new ResponseDataGetByCCAATT()
                                {
                                    Seq = (long)i++,
                                    Hno = obj.GetValue(0).ToString() ?? "",
                                    Village = obj.GetValue(1).ToString() ?? "", //หมู่
                                    Road = obj.GetValue(2).ToString() ?? "", //ถนน
                                    Lane = obj.GetValue(3).ToString() ?? "", //ซอย
                                    Alley = obj.GetValue(4).ToString() ?? "",//ตรอก

                                };
                                roles.Add(res);
                            }
                        }
                    }
                }
                await connection.CloseAsync();
                return roles;

            }
            catch (Exception ex)
            {
                await connection.CloseAsync();
                Console.WriteLine("ERROR:", ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<ResponseAddressData>?> GetAddressByPostcodeWithHNOByRawQueryAsync(string sHNO, string sPostcode)
        {
            var connection = _context.Database.GetDbConnection();
            try
            {
                List<ResponseAddressData> roles = new List<ResponseAddressData>();
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    string query = "SELECT THP_ID, DOPA_HNO, VILLAGE_NO, DOPA_LANE_NAME, DOPA_ROAD_NAME, DOPA_ALLEY_NAME,DOPA_SUB_DISTRICT_NAME, DOPA_DISTRICT_NAME, DOPA_PROVINCE_NAME, THP_POSTCODE " +
                        "FROM ADS_ADDRESS_MASTER WHERE THP_POSTCODE = " + " '" + sPostcode + "'" +
                        " AND DOPA_HNO = " + "'" + sHNO + "'" +
                        "AND INACTIVE = 0 AND EXPIRY_DATE IS NULL ORDER BY THP_POSTCODE,THP_ID,DOPA_HNO";

                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using (var obj = await command.ExecuteReaderAsync())
                    {
                        long i = 1;

                        if (obj.HasRows)
                        {
                            while (await obj.ReadAsync())
                            {
                                var res = new ResponseAddressData()
                                {
                                    Seq = i++,
                                    ThpId = Convert.ToInt64(obj.GetValue(0)),
                                    Hno = obj.GetValue(1).ToString(),
                                    Village = obj.GetValue(2).ToString(),
                                    Lane = obj.GetValue(3).ToString(),
                                    Road = obj.GetValue(4).ToString(),
                                    Alley = obj.GetValue(5).ToString(),
                                    SubDistrict = obj.GetValue(6).ToString(),
                                    District = obj.GetValue(7).ToString(),
                                    Province = obj.GetValue(8).ToString(),
                                    Postcode = obj.GetValue(9).ToString()
                                };
                                roles.Add(res);
                            }
                        }
                    }
                }
                await connection.CloseAsync();
                return roles;

            }
            catch (Exception ex)
            {
                await connection.CloseAsync();
                Console.WriteLine("ERROR:", ex.Message);
            }
            return null;

        }

        public async Task<IEnumerable<ResponseAddressData>?> GetAddressByPostcodeWithSubDistrictRawQueryAsync(string sPostcode, string sSubdistrict)
        {
            var connection = _context.Database.GetDbConnection();
            try
            {
                List<ResponseAddressData> roles = new List<ResponseAddressData>();

                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    string query = "SELECT THP_ID, DOPA_HNO,VILLAGE_NO,DOPA_LANE_NAME,DOPA_ROAD_NAME,DOPA_ALLEY_NAME,DOPA_SUB_DISTRICT_NAME,DOPA_DISTRICT_NAME,DOPA_PROVINCE_NAME,THP_POSTCODE " +
                        "FROM ADS_ADDRESS_MASTER WHERE THP_POSTCODE = " + " '" + sPostcode + "'" +
                        "AND DOPA_SUB_DISTRICT_NAME LIKE " + " '%" + sSubdistrict + "%'" +
                        "AND INACTIVE = 0 AND EXPIRY_DATE IS NULL ORDER BY VILLAGE_NO";

                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using (var obj = await command.ExecuteReaderAsync())
                    {
                        int i = 1;
                        if (obj.HasRows)
                        {
                            while (await obj.ReadAsync())
                            {
                                var responseDetails = new ResponseAddressData
                                {
                                    Seq = i++,
                                    ThpId = (long)Convert.ToInt64(obj.GetValue(0)),
                                    Hno = obj.GetValue(1).ToString(),
                                    Village = obj.GetValue(2).ToString(),
                                    Lane = obj.GetValue(3).ToString(),
                                    Road = obj.GetValue(4).ToString(),
                                    Alley = obj.GetValue(5).ToString(),
                                    SubDistrict = obj.GetValue(6).ToString(),
                                    District = obj.GetValue(7).ToString(),
                                    Province = obj.GetValue(8).ToString(),
                                    Postcode = obj.GetValue(9).ToString()
                                };
                                roles.Add(responseDetails);
                            }
                        }
                    }
                    await connection.CloseAsync();

                    return roles;

                }
            }
            catch (Exception ex)
            {
                await connection.CloseAsync();

                Console.WriteLine("Error", ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<ResponseAddressData>?> GetAddressByPostcodeHnoAndVillageRawQueryAsync(string sPostcode, string sHno, string sVillage)
        {
            var connection = _context.Database.GetDbConnection();
            try
            {
                int Village = (string.IsNullOrEmpty(sVillage)) ? 0 : Convert.ToInt32(sVillage);

                List<ResponseAddressData> roles = new List<ResponseAddressData>();

                //var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    string query = "SELECT THP_ID, DOPA_HNO, VILLAGE_NO, DOPA_LANE_NAME, DOPA_ROAD_NAME, DOPA_ALLEY_NAME, DOPA_SUB_DISTRICT_NAME, DOPA_DISTRICT_NAME, DOPA_PROVINCE_NAME, THP_POSTCODE " +
                        "FROM ADS_ADDRESS_MASTER WHERE THP_POSTCODE = " + " '" + sPostcode.ToString() + "'" +
                        "AND DOPA_HNO = " + " '" + sHno.ToString() + "'" +
                        "AND COALESCE(VILLAGE_NO,0) = " + " '" + Village + "'" +
                        "AND INACTIVE = 0 AND EXPIRY_DATE IS NULL " +
                        "GROUP BY THP_ID, DOPA_HNO, VILLAGE_NO, DOPA_LANE_NAME, DOPA_ROAD_NAME, DOPA_ALLEY_NAME, DOPA_SUB_DISTRICT_NAME, DOPA_DISTRICT_NAME, DOPA_PROVINCE_NAME, THP_POSTCODE " +
                        "ORDER BY THP_POSTCODE, DOPA_HNO ASC";

                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using (var obj = await command.ExecuteReaderAsync())
                    {
                        long i = 1;
                        if (obj.HasRows)
                        {
                            while (await obj.ReadAsync())
                            {
                                var responseDetails = new ResponseAddressData
                                {
                                    Seq = i++,
                                    ThpId = (long)Convert.ToInt64(obj.GetValue(0)),
                                    Hno =  obj.GetValue(1).ToString(),
                                    Village = obj.GetValue(2).ToString(),
                                    Lane = obj.GetValue(3).ToString(),
                                    Road = obj.GetValue(4).ToString(),
                                    Alley = obj.GetValue(5).ToString(),
                                    SubDistrict = obj.GetValue(6).ToString(),
                                    District = obj.GetValue(7).ToString(),
                                    Province = obj.GetValue(8).ToString(),
                                    Postcode = obj.GetValue(9).ToString()
                                };
                                roles.Add(responseDetails);
                            }
                        }
                    }
                    await connection.CloseAsync();

                    return roles;

                }
            }
            catch (Exception ex)
            {
                await connection.CloseAsync();

                Console.WriteLine("Error", ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<ResponseAddressData>?> GetAddressByPostcodeHNOVillageAndSubdistrictRawQueryAsync(string sPostcode, string sHNO, string sVillage, string sSubdistrict)
        {
            var connection = _context.Database.GetDbConnection();
            try
            {
                int Village = (string.IsNullOrEmpty(sVillage)) ? 0 : Convert.ToInt32(sVillage);
                List<ResponseAddressData> rolesAddress = new List<ResponseAddressData>();
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    string query = "SELECT THP_ID, DOPA_HNO, VILLAGE_NO, DOPA_LANE_NAME, DOPA_ROAD_NAME, DOPA_ALLEY_NAME, DOPA_SUB_DISTRICT_NAME, DOPA_DISTRICT_NAME, DOPA_PROVINCE_NAME, THP_POSTCODE " +
                       "FROM ADS_ADDRESS_MASTER WHERE THP_POSTCODE = " + " '" + sPostcode.ToString() + "'" +
                       "AND DOPA_HNO = " + " '" + sHNO.ToString() + "'" +
                       "AND COALESCE(VILLAGE_NO,0) = " + " '" + Village + "'" +
                       "AND DOPA_SUB_DISTRICT_NAME = " + " '" + sSubdistrict.ToString() + "'" +
                       "AND INACTIVE = 0 AND EXPIRY_DATE IS NULL " +
                       "GROUP BY THP_ID, DOPA_HNO, VILLAGE_NO, DOPA_LANE_NAME, DOPA_ROAD_NAME, DOPA_ALLEY_NAME, DOPA_SUB_DISTRICT_NAME, DOPA_DISTRICT_NAME, DOPA_PROVINCE_NAME, THP_POSTCODE " +
                       "ORDER BY THP_POSTCODE, DOPA_HNO ASC";

                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using (var obj = await command.ExecuteReaderAsync())
                    {
                        long i = 1;

                        if (obj.HasRows)
                        {
                            while (await obj.ReadAsync())
                            {
                                var resPostCode = new ResponseAddressData()
                                {
                                    Seq = i++,
                                    ThpId = (long)Convert.ToInt64(obj.GetValue(0)),
                                    Hno = obj.GetValue(1).ToString(),
                                    Village = obj.GetValue(2).ToString(),
                                    Lane = obj.GetValue(3).ToString(),
                                    Road = obj.GetValue(4).ToString(),
                                    Alley = obj.GetValue(5).ToString(),
                                    SubDistrict = obj.GetValue(6).ToString(),
                                    District = obj.GetValue(7).ToString(),
                                    Province = obj.GetValue(8).ToString(),
                                    Postcode = obj.GetValue(9).ToString()
                                };
                                rolesAddress.Add(resPostCode);
                            }
                        }
                    }
                }
                await connection.CloseAsync();
                return rolesAddress;

            }
            catch (Exception ex)
            {
                await connection.CloseAsync();
                Console.WriteLine("ERROR:", ex.Message);
            }
            return null;
        }

    }
}
