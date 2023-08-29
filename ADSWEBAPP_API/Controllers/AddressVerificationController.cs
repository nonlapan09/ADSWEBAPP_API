using ADSWEBAPP_API.Data.Address;
using ADSWEBAPP_API.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace ADSWEBAPP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressVerificationController : Controller
    {
        private readonly ILogger<AddressVerificationController> _logger;
        private readonly AddressRepo _addresRepo;
        private readonly IDistributedCache _cache;
        public AddressVerificationController(AddressRepo addressRepo,ILogger<AddressVerificationController> logger, IDistributedCache cache) {
            _addresRepo = addressRepo;
            _logger = logger;
            _cache = cache;
        
        }
        public IActionResult Index()
        {
            return View();
        }

        
       
    }
}
