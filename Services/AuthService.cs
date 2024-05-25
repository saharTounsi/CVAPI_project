using System.Security.Claims;
using CVAPI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;


namespace CVAPI.Services {
    public class AuthService:IAuthorizationService {
        //readonly static string corsPolicyName="AllowSpecificOrigin";
        public static void addAuthentication(WebApplicationBuilder builder){
            const string AuthenticationSchema=CookieAuthenticationDefaults.AuthenticationScheme;
            builder.Services.AddAuthentication(options=>{
                options.DefaultAuthenticateScheme=AuthenticationSchema;
                options.DefaultSignInScheme=AuthenticationSchema;
                options.DefaultChallengeScheme=AuthenticationSchema;
            }).AddCookie(options=>{
                options.Cookie.HttpOnly=true;
                options.Cookie.SameSite=SameSiteMode.None;
                options.Cookie.SecurePolicy=CookieSecurePolicy.None;
            });
            builder.Services.AddAuthorization(options=>{
                //options.AddPolicy("isAuthenticated",(policy)=>policy.RequireClaim());
                options.AddPolicy("isAdmin",(policy)=>policy.RequireClaim("isAdmin","True"));
                options.AddPolicy("isManager",(policy)=>policy.RequireClaim("role",User.Role.Manager.ToString()));
                options.AddPolicy("isHR",(policy)=>policy.RequireClaim("role",User.Role.HR.ToString()));
                options.AddPolicy("isEmployee",(policy)=>policy.RequireClaim("role",User.Role.Employee.ToString()));
            });
        }

        public  Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user,object? resource,string policyName){
            if(user.Identity!.IsAuthenticated){
                return Task.FromResult(AuthorizationResult.Success());
            }
            else return Task.FromResult(AuthorizationResult.Failed());
        }
        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource, IEnumerable<IAuthorizationRequirement> requirements){
            throw new NotImplementedException();
        }
    }
}
