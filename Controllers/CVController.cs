using CVAPI.Schemas;
using CVAPI.Repositories;
using CVAPI.Data.CVAnalysis;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute=Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Schemas;
using CVAPI.Middlewares;


namespace CVAPI.Controllers {
    [ApiController] [Route("api/cv")]
    public class CVController:Controller {

        private readonly CVRep cvRep;
        private readonly HttpContext context;


        public CVController(CVRep cvRep,IHttpContextAccessor httpContextAccessor){
            this.cvRep=cvRep;
            context=httpContextAccessor.HttpContext!;       
        }

        [HttpGet("user/{userId}")] 
        [Authorize] [Authorize("isAdmin")]
        [ProducesResponseType(200,Type=typeof(List<CVSchema>))]
        public async Task<IActionResult> GetUserCVs(string userId){
            var cvs=await cvRep.GetUserCVs(userId);
            return Ok(await cvRep.toCVSchema(cvs));
        }

        [HttpGet("user")] [Authorize]
        [ProducesResponseType(200,Type=typeof(List<CVSchema>))]
        public async Task<IActionResult> GetUserCVs(){
            var id=context.User.FindFirstValue("id")!;
            var cvs=await cvRep.GetUserCVs(id);
            return Ok(await cvRep.toCVSchema(cvs));
        }

        [HttpGet("all")] 
        [Authorize] [Authorize("isAdmin")]
        [ProducesResponseType(200,Type=typeof(List<CVSchema>))]
        public async Task<IActionResult> GetAllCVs(){
            var cvs=await cvRep.GetAll();
            return Ok(await cvRep.toCVSchema(cvs));
        }


        [HttpGet("{cvId}")] 
        [Authorize] [Authorize("isAdmin")]
        [ProducesResponseType(200,Type=typeof(CVSchema))]
        public async Task<IActionResult> GetCV(string cvId){
            var cv=await cvRep.GetCV(cvId);
            if(cv!=null) return Ok(await cvRep.toCVSchema(cv));
            else throw new Error("no such cv");
        }

        [HttpPost("upload")] [Authorize]
        [ProducesResponseType(200,Type=typeof(CVSchema))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UploadCV([FromForm] NewCVData data){
            var userId=context.User.FindFirstValue("id")!;
            CVData cvdata=await CVAnalyser.analyseCV(data.file);
            var cv=await cvRep.AddCV(userId,data.name,cvdata);
            return Ok(await cvRep.toCVSchema(cv));
        }
    }
}
