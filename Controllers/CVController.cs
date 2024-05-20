using Azure;
using CVAPI.Interfaces;
using CVAPI.Schemas;
using CVAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;


namespace CVAPI.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CVController:Controller {

        private readonly ICVRep cvRep;
        private readonly HttpContext context;


        public CVController(ICVRep cvRep,IHttpContextAccessor httpContextAccessor){
            this.cvRep=cvRep;
            context=httpContextAccessor.HttpContext!;       
        }

        [HttpGet("user/{id}")]
        [ProducesResponseType(200,Type=typeof(List<CVSchema>))]
        public IActionResult GetUserCVs(string id){
            var cvs=cvRep.GetUserCVs(id);
            return Ok(cvs);
        }
        
    }
}
