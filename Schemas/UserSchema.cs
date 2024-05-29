using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CVAPI.Models;


namespace CVAPI.Schemas {
    public class UserSchema:NewUserSchema {

        [Key] [Required] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id {get;set;}

        private bool? _isAdmin {get;set;}=null;
        public bool isAdmin {
            get { 
                return _isAdmin??role==User.Role.Manager;
            }
            set {
                _isAdmin=value;
            }
        }

        [DefaultValue(true)]
        public bool isActive {get;set;}=true;

        public UserSchema(){}
        public UserSchema(NewUserSchema data){
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
