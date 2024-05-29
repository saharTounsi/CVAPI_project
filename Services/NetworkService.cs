using System.Net;
using System.Net.Sockets;


namespace CVAPI.Services {
    public class NetworkService {
        public static string addCors(WebApplicationBuilder builder){
            var isDevEnv=builder.Environment.IsDevelopment();
            //var clientURL=builder.Configuration.GetValue<string>($"clientURL:{(isDevEnv?"dev":"prod")}");
            string corsPolicyName="AllowSpecificOrigin";
            builder.Services.AddCors(options=>{
                options.AddPolicy(corsPolicyName,policy=>{
                    policy.WithOrigins("http://localhost:3000");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                });
            });
            return corsPolicyName;
        }

        public static string getLocalIPAddress(){
            var host=Dns.GetHostEntry(Dns.GetHostName());
            foreach(var ip in host.AddressList){
                if (ip.AddressFamily==AddressFamily.InterNetwork){
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
