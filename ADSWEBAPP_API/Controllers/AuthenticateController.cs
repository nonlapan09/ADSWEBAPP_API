using ADSWEBAPP_API.Config;
using ADSWEBAPP_API.Data.Address;
using ADSWEBAPP_API.Data.Authentication;
using ADSWEBAPP_API.Dto;
using ADSWEBAPP_API.Dto.AuthenData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace ADSWEBAPP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerControllerOrder(1)]

    public class AuthenticateController : ControllerBase
    {
        //private readonly UserManager<ApplicationUser> userManager;
        //private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        private readonly ILogger<AddressController> _logger;

        private JwtConfig _jwtConfig;
        private AuthenticationRepo _AuthenticationRepo;
        private AuthenticationDbContext _context;

        public AuthenticateController(
            ILogger<AddressController> logger
            , IConfiguration configuration
            , IOptions<JwtConfig> jwtConfig
            , AuthenticationRepo authentication,
AuthenticationDbContext context)
        {
            //this.userManager = userManager;
            //this.roleManager = roleManager;
            _logger = logger;
            _configuration = configuration;
            _jwtConfig = jwtConfig.Value;
            _AuthenticationRepo = authentication;
            _context = context;
        }

        [HttpPost]
        [Route("/ads/[controller]/[action]/")]
        public ActionResult SignUp(SignUpUser model)
        {
            if (ModelState.IsValid)
            {
                if (_context.dbMasterAuthentication.Any(u => u.Username == model.Username))
                {
                    return BadRequest("Username already exists. Please Enter Other username.");
                }

                var data = new MasterAuthentication()
                {
                    Username = model.Username ?? "",
                    Password = _AuthenticationRepo.EncyptPassword(model.Password!),
                    firstname = model.Firstname ?? "",
                    lastname = model.Lastname ?? "",
                    email = model.Email ?? "",
                    Startdate = DateTime.UtcNow,
                    ExeDate = DateTime.UtcNow.AddYears(1),
                    Inactive = (int)0
                };
                _context.dbMasterAuthentication.Add(data);
                _context.SaveChanges();
                _logger.LogInformation("SIGN UP SUCCESSFULLY | WELCOME : " + model.Username);
                return Ok("SIGN UP SUCCESSFULLY | WELCOME : " + model.Username);
            }
            else
            {
                _logger.LogError("sign up invalid");
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/ads/[controller]/[action]/")]
       
        public ActionResult<ResponseModel> Login([FromBody] LoginModel model)
        {
            _logger.LogInformation("Authen Login: | Start | ");
            _logger.LogInformation("Authen Login: | Process | " + model.Username);
            var user = _AuthenticationRepo.checkLogin(model);
           
            if (user != null)
            {
                //bool isValid = user.Any(u=> u.Username == model.Username && DecyptPassword(u.Password) == model.Password);
                
                    //var isValid = (user.Username == model.Username && user.Password == model.Password);
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username ?? ""),
                    //new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                    //foreach (var userRole in userRoles)
                    //{
                    //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    //}

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        //expires: DateTime.Now.AddHours(3),
                        //expires: DateTime.UtcNow.AddMinutes(5),
                        expires: DateTime.UtcNow.AddHours(24),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    _logger.LogInformation("Authen Login: | End | " + model.Username);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo

                    });
                
            }
            return Unauthorized();
        }

    }
}
