using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CVAPI.Models;


namespace CVAPI.Schemas {
    public class NewUserSchema {
        [Required]
        public string firstName {get;set;}

        [Required]
        public string lastName {get;set;}

        [Required]
        public string email {get;set;}
        
        [Required]
        public User.Role role {get;set;}
         
    }
}