using CVAPI.Schemas;
using CVAPI.Interfaces;
using CVAPI.Data.CVAnalysis;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute=Microsoft.AspNetCore.Mvc.RouteAttribute;


namespace CVAPI.Controllers {
    [ApiController] [Route("api/cv")]
    public class CVController:Controller {

        private readonly ICVRep cvRep;
        private readonly HttpContext context;


        public CVController(ICVRep cvRep,IHttpContextAccessor httpContextAccessor){
            this.cvRep=cvRep;
            context=httpContextAccessor.HttpContext!;       
        }

        [HttpPost("upload")]
        [ProducesResponseType(200,Type=typeof(CVData))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UploadCV(IFormFile file){
            try{
                CVData cvdata=await CVAnalyser.analyseCV(file);
                return Ok(cvdata);
            }
            catch(Exception exception){
                return BadRequest(exception.Message);
            }
        }


        [HttpGet("user/{id}")]
        [ProducesResponseType(200,Type=typeof(List<CVSchema>))]
        public IActionResult GetUserCVs(string id){
            var cvs=cvRep.GetUserCVs(id);
            return Ok(cvs);
        }
        
    }
}
