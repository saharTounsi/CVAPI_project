using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CVAPI.Services;


namespace Models {
    public class VerficationLink {
        
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id {get;set;}

        public string userId {get;set;}

        public Type type {get;set;}

        public DateTime expiryDateTime {get;set;}

        public enum Type {PasswordReset};

        public string toURL(){
            string ipaddress=SecurityService.getLocalIPAddress();
            return $"http://{ipaddress}:8080/user/passwordreset?id={id}&type={type}&userId={userId}";
        }
    }
}
