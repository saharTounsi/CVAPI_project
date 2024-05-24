using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CVAPI.Models;


namespace CVAPI.Schemas {
    public class UserSchema:UserSignUpSchema {

        [Key] [Required] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }

        [DefaultValue(false)]
        public bool isAdmin {get;set;}=false;

        [DefaultValue(true)]
        public bool isActive {get;set;}=true;

        public UserSchema(){}
        public UserSchema(UserSignUpSchema data){
            email=data.email;
            firstName=data.firstName;
            lastName=data.lastName;
            role=data.role;
        }
        
        public UserSchema(User user){
            id=user.id;
            isAdmin=user.isAdmin;
            isActive=user.isActive;
            email=user.email;
            firstName=user.firstName;
            lastName=user.lastName;
            role=user.role;
        }
    }
}
