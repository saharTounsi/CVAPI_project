

namespace CVAPI.Services {
    public class SecurityService {

        public static string addCors(WebApplicationBuilder builder){
            var isDevEnv=builder.Environment.IsDevelopment();
            var clientURL=builder.Configuration.GetValue<string>($"clientURL:{(isDevEnv?"dev":"prod")}");
            string corsPolicyName="AllowSpecificOrigin";
            builder.Services.AddCors(options=>{
                options.AddPolicy(corsPolicyName,policy=>{
                    policy.WithOrigins(clientURL??"*");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                });
            });
            return corsPolicyName;
        }
    }
}
