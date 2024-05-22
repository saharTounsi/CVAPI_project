using CVAPI.Interfaces;
using CVAPI.Models;
using CVAPI.Schemas;
using Microsoft.AspNetCore.DataProtection;


namespace CVAPI.Services {
    public class AuthService {

        static readonly string protectorPurpose="auth-cookie";
            
        private readonly HttpContext httpContext;
        //private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDataProtectionProvider dataProtectionProvider;

        public AuthService(IHttpContextAccessor httpContextAccessor,IDataProtectionProvider dataProtectionProvider){
            //this.httpContextAccessor=httpContextAccessor;
            this.dataProtectionProvider=dataProtectionProvider;
            this.httpContext=httpContextAccessor.HttpContext!;
        }

        public AuthData logUserIn(User user){
            var userId=user.id;
            var sessionId="session_id";
            var protector=dataProtectionProvider.CreateProtector(protectorPurpose);
            httpContext.Response.Headers["set-cookie"]=$"userId={protector.Protect(userId)};httpOnly=true;secure=true";
            httpContext.Response.Headers["set-cookie"]=$"sessionId={protector.Protect(sessionId)};httpOnly=true;secure=true";
            return new AuthData(){
                userId=userId,
                sessionId=sessionId,
            };
        }

        public AuthData authenticateUser(){
            var protector=dataProtectionProvider.CreateProtector(protectorPurpose);
            var userIdCookie=httpContext.Request.Headers.Cookie.FirstOrDefault(c=>c.StartsWith("userId="));
            var sessionIdCookie=httpContext.Request.Headers.Cookie.FirstOrDefault(c=>c.StartsWith("sessionId="));;
            if((userIdCookie!=null)&&(sessionIdCookie!=null)){
                var protectedUserIdPayload=userIdCookie.Split("=").Last();
                var protectedSessionIdPayload=sessionIdCookie.Split("=").Last();
                var userIdPayload=protector.Unprotect(protectedUserIdPayload);
                var sessionIdPayload=protector.Unprotect(protectedSessionIdPayload);
                string userId=userIdPayload.Split(":").Last();
                string sessionId=sessionIdPayload.Split(":").Last();
                if(userId!=null) return new AuthData(){
                    userId=userId,
                    sessionId=sessionId,
                };
                else throw new Exception("unknow user identifier");
            }
            else throw new Exception("unauthenticated action");
        }
    }
}
