using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CVAPI.Models;


namespace CVAPI.Schemas {
    public class UserSignUpSchema:UserUpdateSchema {
        
        [Required]
        public string email { get; set; }
        
        [Required]
        public User.Role role { get; set; }
         
    }
}