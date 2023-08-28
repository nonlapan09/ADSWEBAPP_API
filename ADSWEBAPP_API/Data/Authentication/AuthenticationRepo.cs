using ADSWEBAPP_API.Dto.AuthenData;
using System.Text;

namespace ADSWEBAPP_API.Data.Authentication
{
    public class AuthenticationRepo
    {
        private AuthenticationDbContext _context;

        public AuthenticationRepo(AuthenticationDbContext context)
        {
            _context = context;
        }

        //check user/pass in database
        public IEnumerable<LoginModel>? checkLogin(LoginModel model)
        {
            var items = _context.dbMasterAuthentication
                .Where(w => w.Username == model.Username)
                .Where(w => w.Password == EncyptPassword(model.Password!))
                .Where(w => w.Startdate <= DateTime.Now && w.ExeDate >= DateTime.Now)
                .Where(w => w.Inactive == 0)
                .Take(1)
                .ToList();
            if (items.Count > 0)
            {
                var loginDetail = items.Select((s, Index) => new LoginModel
                {
                    Username = s.Username ?? "",
                    Password = s.Password ?? ""
                }).ToList().AsQueryable();

                return loginDetail;
            }
            else
            {
                return null;
            }
        }

        //เข้ารหัส 
        public static string Key = "sdkfsomvpsdv@3pr4t50";
        public string EncyptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }
            else
            {
                password += Key;
                byte[] storePassword = Encoding.UTF8.GetBytes(password);
                string encryptedPassword = Convert.ToBase64String(storePassword);
                return encryptedPassword;
            }
        }
        public static string DecyptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }
            else
            {
                byte[] encryptedPasswordByte = Convert.FromBase64String(password);
                string decryptedPassword = Encoding.UTF8.GetString(encryptedPasswordByte);
                decryptedPassword = decryptedPassword.Substring(0, decryptedPassword.Length - Key.Length);
                return decryptedPassword;
            }
        }

    }
}
