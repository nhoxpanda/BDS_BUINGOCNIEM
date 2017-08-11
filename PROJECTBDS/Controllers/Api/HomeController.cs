using System.Web.Http;
using PROJECTBDS.DTO;
using PROJECTBDS.Services.Home;

namespace PROJECTBDS.Controllers.Api
{

    public class HomeController : ApiController
    {
        private readonly HomeServices _context;
        public HomeController()
        {
            _context = new HomeServices();
        }
        [HttpPost]
        public IHttpActionResult GetDistrict(HomeDto dto)
        {
            var districts = _context.GetDistricts(dto.ProvinceId);
            
            return Ok(districts);
        }
    }
}
