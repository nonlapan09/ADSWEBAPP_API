using ADSWEBAPP_API.Dto;
using ADSWEBAPP_API.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using StackExchange.Redis;

namespace ADSWEBAPP_API.Data.Address
{
    public class AddressRepo
    {
        private readonly ApplicationDbContext _context;
        public AddressRepo(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<AddressProvinceResponse?> GetAddressProvinceWithProvinceCodeAsync(string provinceCode, int page = 1, int pageSize = 20) 
        {
            try 
            {
                var addressProvince = await _context.DbMasterAddress.AsNoTracking().Where(w=>w.ProvinceCode.Equals(provinceCode))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var total = await _context.DbMasterAddress.Where(w => w.ProvinceCode.Equals(provinceCode)).AsNoTracking().CountAsync();
                AddressProvinceResponse addressProvinceResponse = new()
                {
                   Total = total,
                   Data = addressProvince
                };
                return addressProvinceResponse;
            }
            catch { throw; }
        }
        public async Task<string?> GetAddressProvinceWithProvinceCodeRawQAsync(string provinceCode, int page = 1, int pageSize = 20)
        {
            try
            {
                string roles = "";
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                string cmd = "SELECT COUNT(TBL_ID)";
                cmd += " FROM TBL_MASTER_ADDRESS";
                cmd += " WHERE DOPA_PROVINCE_CODE =";
                cmd += $"'{provinceCode}'";
                command.CommandText = cmd;
                command.CommandType = CommandType.Text;
                using var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows) 
                {
                    while (await reader.ReadAsync()) 
                    {
                        var res = new MasterAddressMainModel();
                        roles = reader.GetValue(0).ToString() ?? "";
                    }
                }
                await connection.CloseAsync();
                return roles;
                /* var addressProvinceResponse = new AddressProvinceResponse
                 {
                     Total = result.Total,
                     Data = await query.Select(a => a.Data).ToListAsync()
                 };*/

            }
            catch
            {
                throw;
            }
        }
        public async Task<List<MasterProvinceMainModel>?> GetProvinceAsync() 
        {
            try 
            {
                var province = await _context.DbMasterProvince.AsNoTracking().ToListAsync();
                return province;
            }
            catch { throw; }
        }

        public async Task<List<MasterDistrictMainModel>?> GetDistrictWithProvinceAsync(string provinceCode) 
        {
            try 
            {
                var district = await _context.DbMasterDistrict.AsNoTracking().Where(w=>w.ProvinceCode.Equals(provinceCode)).ToListAsync();
                return district;
            }
            catch { throw; }
        }

        public async Task<List<MasterDistrictMainModel>?> GetSubDistrictWithProvinceAsync(string districtCode)
        {
            try
            {
                var district = await _context.DbMasterDistrict.AsNoTracking().Where(w => w.DistrictCode.Equals(districtCode)).ToListAsync();
                return district;
            }
            catch { throw; }
        }
    }
}
