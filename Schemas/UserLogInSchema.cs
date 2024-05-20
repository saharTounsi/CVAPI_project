using System.ComponentModel.DataAnnotations;


namespace CVAPI.Schemas {
    public class UserLogInSchema {
        
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
        