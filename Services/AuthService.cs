using System.Net;
using CVAPI.Middlewares;
using CVAPI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace CVAPI.Services {
    public class AuthService {
        
        public static void addAuthentication(WebApplicationBuilder builder){
            const string AuthenticationSchema=CookieAuthenticationDefaults.AuthenticationScheme;
            builder.Services.AddAuthentication(options=>{
                options.DefaultAuthenticateScheme=AuthenticationSchema;
                options.DefaultSignInScheme=AuthenticationSchema;
                options.DefaultChallengeScheme=AuthenticationSchema;
            }).AddCookie(options=>{
                options.Cookie.HttpOnly=true;
                options.Cookie.Domain="";
                options.Cookie.SameSite=SameSiteMode.Lax;
                options.Cookie.SecurePolicy=CookieSecurePolicy.None;
                options.Events=new CookieAuthenticationEvents(){
                    OnRedirectToLogin=(context)=>{
                        context.Response.StatusCode=(int)HttpStatusCode.Forbidden;
                        return context.Response.WriteAsync(new Error(){
                            code=0,
                            message="authentication required",
                        }.ToString());
                    },
                    OnRedirectToAccessDenied=(context)=>{
                        context.Response.StatusCode=(int)HttpStatusCode.Unauthorized;
                        return context.Response.WriteAsync(new Error(){
                            code=1,
                            message="access denied, Unauthorized action",
                        }.ToString());
                    },
                };
            });
            builder.Services.AddAuthorization(options=>{
                //options.AddPolicy("isAuthenticated",(policy)=>policy.RequireClaim());
                options.AddPolicy("isAdmin",(policy)=>policy.RequireClaim("isAdmin","True"));
                options.AddPolicy("isManager",(policy)=>policy.RequireClaim("role",User.Role.Manager.ToString()));
                options.AddPolicy("isHR",(policy)=>policy.RequireClaim("role",User.Role.HR.ToString()));
                options.AddPolicy("isEmployee",(policy)=>policy.RequireClaim("role",User.Role.Employee.ToString()));
            });
        }
    }
}
