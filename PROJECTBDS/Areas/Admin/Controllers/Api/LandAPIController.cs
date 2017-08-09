using System.Web.Http;
using PROJECTBDS.Areas.Admin.Dto;
using PROJECTBDS.Areas.Admin.Services;

namespace PROJECTBDS.Areas.Admin.Controllers.Api
{
    public class LandController : ApiController
    {
        private readonly LandServices _context;
        public LandController()
        {
            _context = new LandServices();
        }
        [HttpPost]
        public IHttpActionResult GetDistrict(LandDto dto)
        {
            var districts = _context.GetDistricts(dto.ProvinceId);

            return Ok(districts);
        }
    }

    public class WardController : ApiController
    {
        private readonly LandServices _context;
        public WardController()
        {
            _context = new LandServices();
        }
        [HttpPost]
        public IHttpActionResult GetWards(LandDto dto)
        {
            var districts = _context.GetWards(dto.ProvinceId);

            return Ok(districts);
        }
    }


    public class ProjectController : ApiController
    {
        private readonly LandServices _context;
        public ProjectController()
        {
            _context = new LandServices();
        }
       
        [HttpPost]
        public IHttpActionResult GetProject(ProjectDto dto)
        {
            
            return Ok(_context.GetProjectDetail(dto.ProjectId, dto.DictionaryId));
        }
    }
}
