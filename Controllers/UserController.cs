
using CVAPI.Interfaces;
using CVAPI.Models;
using CVAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CVAPI.Data;
using CVAPI.Schemas;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CVAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController:Controller {
        
        private readonly IUserRep userRep;
        private readonly HttpContext context;


        public UserController(IUserRep userRep,IHttpContextAccessor httpContextAccessor){
            this.userRep=userRep;
            context=httpContextAccessor.HttpContext!;           
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserSchema))]  
        [ProducesResponseType(400)]
        public IActionResult GetUsers(string id){
            var users=userRep.GetUsers(id);
            return Ok(users);
        }


        [HttpPost("signup")]
        //[Authorize(Roles="User")]
        //[ProducesResponseType(204)]
        [ProducesResponseType(200,Type=typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser([FromBody] UserSignUpSchema data){
            var user=await userRep.CreateUser(data);
            return Ok(true);
        }

        [HttpPost("login")]
        [ProducesResponseType(200,Type=typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> login([FromBody] UserLogInSchema data){
            return Ok("email");
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(200,Type=typeof(UserSchema))]
        public async Task<IActionResult> DeleteUser(string id){
            var user=await userRep.DeleteUser(id);
            return Ok (true);
            
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200,Type=typeof(UserSchema))]
        public async Task<IActionResult>UpdateUser(string id,[FromBody] UserUpdateSchema data){
           var user=await userRep.UpdateUser(id ,data);
           return Ok (true);
        }
    }
}
