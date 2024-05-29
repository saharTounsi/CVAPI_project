using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CVAPI.Schemas;
using Microsoft.AspNetCore.Identity;


namespace CVAPI.Models {
    public class User : UserSchema {
        //Props
        [Required]
        public string hash {get;set;}

        public string? loginOTP {get;set;}

        public User(){}
        public User(NewUserSchema data):base(data){}

        //Relationships
        [JsonIgnore]
        public List<CV> cvs { get; set; }
        
        [JsonIgnore]
        public List<CVModif> cvModifs { get; set; }

        [JsonIgnore]
        public List<CVExport> cvExports { get; set; }


        public enum Role {Employee,HR,Manager};

        static public string getHash(User user,string password="1234"){
            var hasher=new PasswordHasher<User>();
            return hasher.HashPassword(user,password);
        }

        static public bool verifyPassword(User user,string password){
            var hasher=new PasswordHasher<User>();
            return hasher.VerifyHashedPassword(user,user.hash,password)>0;
        }
    }
}
