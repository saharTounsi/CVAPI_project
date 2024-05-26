using CVAPI.Models;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using CVAPI.Data;
using CVAPI.Schemas;
using CVAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CVAPI.Middlewares;



namespace CVAPI.Controllers {
    [ApiController] [Route("api/cvversion")]
    public class CVVersionController:Controller {

        private readonly HttpContext context;
        private readonly CVVersionRep versionRep;


        public CVVersionController(CVVersionRep versionRep,IHttpContextAccessor httpContextAccessor){
            this.versionRep=versionRep;
            context=httpContextAccessor.HttpContext!;       
        }
        [HttpGet("cv/{cvId}")] [Authorize]
        [ProducesResponseType(200,Type=typeof(List<CVVersionSchema>))]
        public async Task<IActionResult> GetCVVersions(string cvId){
            var cv=await versionRep.FindCV(cvId);
            if(cv!=null){
                var userId=context.User.FindFirstValue("id")!;
                var isAdmin=context.User.FindFirstValue("isAdmin")=="True";
                if((cv.userId==userId)||isAdmin){
                    var versions=await versionRep.FindAllByCVId(cvId);
                    return Ok(versions);
                }
                else throw new Error("only the user himself or an admin can check the cv versions");
            }
            else throw new Error("no such cv");
        }
    }
}