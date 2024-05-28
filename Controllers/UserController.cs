﻿using CVAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using CVAPI.Schemas;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using CVAPI.Middlewares;
using Services;


namespace CVAPI.Controllers {
    [ApiController] [Route("api/user")]
    public class UserController:Controller {
        
        private readonly UserRep userRep;
        private readonly HttpContext context;
        private readonly MailService mailService;


        public UserController(UserRep userRep,IHttpContextAccessor httpContextAccessor,MailService mailService){
            this.userRep=userRep;
            this.context=httpContextAccessor.HttpContext!;
            this.mailService=mailService; 
        }

        [HttpPost("resetpassword")] 
        public async Task<IActionResult> ResetPassword([FromBody] string email){
            await mailService.sendMail(new(){
                toEmail=email,
                subject="Dotnet MailService Test",
                body="a7la mail",
            });
            return Ok(true);
        }

        [HttpPut("update")] [Authorize]
        [ProducesResponseType(200,Type=typeof(bool))]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateFields data){
            var id=User.FindFirst("id")!.Value;
            var userId=data.userId??id;
            if(userId==id){
                if(data.role!=null) throw new Error("user can't change his own role");
            }
            else{
                if(data.password!=null) throw new Error("only the user himself can change his password");
                else{
                    var isAdmin=User.FindFirst("isAdmin")!.Value=="True";
                    if(!isAdmin) throw new Error("admin status required");
                }
            }
            data.userId=userId;
            var sucessful=await userRep.UpdateUser(data);
            return Ok(sucessful);
        }

        [HttpPost("add")] 
        [Authorize] [Authorize(Policy="isAdmin")]
        [ProducesResponseType(200,Type=typeof(UserSchema))]
        public async Task<IActionResult>AddUser([FromBody] NewUserSchema data){
           var user=await userRep.AddUser(data);
           return Ok(new UserSchema(user));
        }

        [HttpGet("{userId}")] 
        [Authorize] [Authorize(Policy="isAdmin")]
        [ProducesResponseType(200,Type=typeof(UserSchema))]
        public async Task<IActionResult> GetUser(string userId){
            var user=await userRep.GetUser(userId);
            if(user!=null) return Ok(new UserSchema(user));
            else throw new Error("no such user") ;
        }

        [HttpGet] [Authorize]
        [ProducesResponseType(200,Type=typeof(UserSchema))]
        public async Task<IActionResult> GetUser(){
            var userId=context.User.FindFirst("id")!.Value;
            var user=await userRep.GetUser(userId);
            return Ok(user==null?null:new UserSchema(user));
        }

        [HttpGet("logout")] [Authorize]
        [ProducesResponseType(200,Type=typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> logout(){
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(true);
        }

        [HttpGet("all")] 
        [Authorize] [Authorize(Policy="isAdmin")] 
        [ProducesResponseType(200,Type=typeof(List<UserSchema>))]  
        public async Task<IActionResult> GetUsers(){
            var userId=context.User.FindFirstValue("id")!;
            var users=await userRep.GetUsers(userId);
            return Ok(users);
        }

        [HttpPost("login")]
        [ProducesResponseType(200,Type=typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> login([FromBody] UserCredentials data){
            var user=await userRep.FindByCredentials(data);
            var claims=new List<Claim>{
                new(ClaimTypes.NameIdentifier,user.id),
                new("id",user.id),
                new("email",data.email),
                new("isAdmin",user.isAdmin.ToString()),
                new("role",user.role.ToString()),
            };
            var identity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );
            return Ok(true);
        }
    }
}
