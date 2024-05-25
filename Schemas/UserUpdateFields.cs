using CVAPI.Models;


namespace CVAPI.Schemas {
    public class UserUpdateFields {
        
        public string? userId {get;set;} 

        public string? firstName {get;set;}

        public string? lastName {get;set;}

        public string? password {get;set;}

        public User.Role? role {get;set;}

        public bool? isActive {get;set;}
    }
}