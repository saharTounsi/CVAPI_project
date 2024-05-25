using CVAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using CVAPI.Schemas;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using CVAPI.Middlewares;


namespace CVAPI.Controllers {
    [ApiController] [Route("api/user")]
    public class UserController:Controller {
        
        private readonly UserRep userRep;
        private readonly HttpContext context;


        public UserController(UserRep userRep,IHttpContextAccessor httpContextAccessor){
            this.userRep=userRep;
            this.context=httpContextAccessor.HttpContext!;           
        }

        [HttpPost("login")]
        [ProducesResponseType(200,Type=typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> login([FromBody] UserCredentials data){
            var user=await userRep.FindByCredentials(data);
            if(user!=null){
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
            else throw new Error("unrecognized user");
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
            var users=await userRep.GetUsers();
            return Ok(users);
        }

        [HttpGet("")] [Authorize]
        [ProducesResponseType(200,Type=typeof(UserSchema))]
        public async Task<IActionResult> GetUser(){
            var userId=context.User.FindFirst("id")!.Value;
            var user=await userRep.GetUser(userId);
            return Ok(user==null?null:new UserSchema(user));
        }

        [HttpGet("{userId}")] 
        [Authorize] [Authorize(Policy="isAdmin")]
        [ProducesResponseType(200,Type=typeof(UserSchema))]
        public async Task<IActionResult> GetUser(string userId){
            var user=await userRep.GetUser(userId);
            if(user!=null) return Ok(new UserSchema(user));
            else throw new Error("no such user") ;
        }

        [HttpPost("add")] 
        [Authorize] [Authorize(Policy="isAdmin")]
        [ProducesResponseType(200,Type=typeof(UserSchema))]
        public async Task<IActionResult>AddUser([FromBody] NewUserSchema data){
           var user=await userRep.AddUser(data);
           return Ok(new UserSchema(user));
        }
    }
}
