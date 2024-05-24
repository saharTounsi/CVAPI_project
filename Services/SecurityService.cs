

namespace CVAPI.Services {
    public class SecurityService {

        //readonly static string corsPolicyName="AllowSpecificOrigin";

        

        public static string addCors(WebApplicationBuilder builder){
            var isDevEnv=builder.Environment.IsDevelopment();
            string corsPolicyName="AllowSpecificOrigin";
            builder.Services.AddCors(options=>{
                options.AddPolicy(corsPolicyName,policy=>{
                    policy.WithOrigins(isDevEnv?"*":"");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });
            return corsPolicyName;
        }
    }
}
