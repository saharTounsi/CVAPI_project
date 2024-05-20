using System.ComponentModel.DataAnnotations;
using CVAPI.Models;


namespace CVAPI.Schemas {
    public class PersonSchema {
        
        [Required]
        public string id { get; set; }

        [Required]
        public string name { get; set; }
        
        [Required]
        public User.Role role { get; set; }

        public PersonSchema(){}
        public PersonSchema(User user){
            this.id=user.id;
            this.name=user.name;
            this.role=user.role;
        }
    }
}