using CVAPI.Interfaces;
using CVAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using CVAPI.Data;
using Microsoft.EntityFrameworkCore;
using CVAPI.Schemas;



namespace CVAPI.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CVModifController:Controller 
    {
         private readonly ICVModifRep cvModifRep;
        private readonly HttpContext context;

    
        public CVModifController(ICVModifRep cvModifRep,IHttpContextAccessor httpContextAccessor){
            this.cvModifRep=cvModifRep;
            context=httpContextAccessor.HttpContext!;       
        }
        
        [HttpGet("cv/{id}")]
         [ProducesResponseType(200,Type=typeof(CVModifSchema))]
        public async Task<IActionResult> GetcvModifsCV(string id){
            var CV=await cvModifRep.GetcvModifCV(id);

            return Ok(CV);
        }
        /*  [HttpGet("user/{id}")]
         [ProducesResponseType(200,Type=typeof(List<CVModifSchema>))]
        public IActionResult GetcvModifseditor(string id){
            var cvModifs=context.cvModifs.Where<CVModif>(cVModifs=>cVModifs.editorId==id).ToList<CVModifSchema>();
            
            return Ok(cvModifs);
        } */
    }

    
}