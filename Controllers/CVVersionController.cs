using CVAPI.Interfaces;
using CVAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using CVAPI.Data;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using CVAPI.Schemas;



namespace CVAPI.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CVVersionController: Controller {
         private readonly DataContext context;
        public CVVersionController(DataContext dataContext) {
            context = dataContext;
        }
        [HttpGet("cv/{id}")]
        [ProducesResponseType(200,Type=typeof(List<CVVersionSchema>))]
        public IActionResult GetCVcVVersions(string id){
            var cVVersions=context.cvVersions.Where<CVVersion>(cVVersions=>cVVersions.cvId==id).ToList<CVVersionSchema>();
            return Ok(cVVersions);
        }
    }
}