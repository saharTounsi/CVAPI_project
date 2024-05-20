using CVAPI.Interfaces;
using CVAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using CVAPI.Data;
using Microsoft.EntityFrameworkCore;
using CVAPI.Schemas;
using CVAPI.Repositories;


namespace CVAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CVExportController:Controller 
    {
        private readonly ICVExportRep cvExportRep;
        private readonly HttpContext context;


        public CVExportController(ICVExportRep cvExportRep,IHttpContextAccessor httpContextAccessor){
            this.cvExportRep=cvExportRep;
            context=httpContextAccessor.HttpContext!;       
        }
        
        [HttpGet("user/{id}")]
        [ProducesResponseType(200,Type=typeof(UserSchema))]
        public async Task<IActionResult> GetCVExportUser(string id) {
            var user=await cvExportRep.GetCVExportUser(id);
            return Ok(user);
        }
         
        [HttpGet("Version/{id}")]
        [ProducesResponseType(200,Type=typeof(CVVersionSchema))]
        public async Task<IActionResult> GetCVExportVersion(string id){
            var CVVersion=await cvExportRep.GetCVExportVersion(id);
            
            return Ok(CVVersion);
        } 
    }
}
