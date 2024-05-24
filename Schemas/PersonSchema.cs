using System.ComponentModel.DataAnnotations;
using CVAPI.Models;


namespace CVAPI.Schemas {
    public class PersonSchema {
        
        [Required]
        public string id { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }
        
        [Required]
        public User.Role role { get; set; }

        public PersonSchema(){}
        public PersonSchema(User user){
            this.id=user.id;
            this.firstName=user.firstName;
            this.lastName=user.lastName;
            this.role=user.role;
        }
    }
}