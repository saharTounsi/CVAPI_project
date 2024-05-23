
using CVAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CVAPI.Schemas;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CVAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller {
        
        private readonly IUserRep userRep;
        private readonly HttpContext context;


        public UserController(IUserRep userRep,IHttpContextAccessor httpContextAccessor){
            this.userRep=userRep;
            this.context=httpContextAccessor.HttpContext!;           
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
        [ProducesResponseType(200,Type=typeof(AuthData))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> login([FromBody] UserCredentials data){
            try{
                var user=await userRep.FindByCredentials(data);
                if(user!=null){
                    var claims=new List<Claim>{new(ClaimTypes.Name,data.email)};
                    var identity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    await context.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity)
                    );
                    return Ok(new AuthData(){userId=user.id}); 
                }
                else throw new Exception("unrecognized user");
            }
            catch(Exception exception){
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("logout")]
        [ProducesResponseType(200,Type=typeof(bool))]
        [ProducesResponseType(400)]
        //[Authorize("")]
        public async Task<IActionResult> logout([FromBody] UserCredentials data){
            try{
                var isAuthenticated=context.User.Identity!.IsAuthenticated;
                if(isAuthenticated){
                    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return Ok(true);
                }
                else throw new Exception("unknown session");
            }
            catch(Exception exception){
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200,Type=typeof(UserSchema))]
        public async Task<IActionResult> DeleteUser(string id){
            var user=await userRep.DeleteUser(id);
            return Ok(true);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200,Type=typeof(UserSchema))]
        public async Task<IActionResult>UpdateUser(string id,[FromBody] UserUpdateSchema data){
           var user=await userRep.UpdateUser(id,data);
           return Ok(true);
        }
    }
}
