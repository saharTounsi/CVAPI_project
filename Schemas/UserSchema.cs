using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CVAPI.Models;


namespace CVAPI.Schemas {
    public class UserSchema:UserSignUpSchema {

        [Key] [Required] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }

        public string? adminId { get; set; }

        public UserSchema(){}
        public UserSchema(UserSignUpSchema data){
            email=data.email;
            password=data.password;
            name=data.name;
            role=data.role;
        }
        
        public UserSchema(User user){
            id=user.id;
            adminId=user.adminId;
            email=user.email;
            name=user.name;
            role=user.role;
        }
    }
}
