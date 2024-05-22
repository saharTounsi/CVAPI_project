using System.ComponentModel.DataAnnotations;


namespace CVAPI.Schemas {
    public class AuthData {
        
        [Required]
        public string userId { get; set; }

        [Required]
        public string sessionId { get; set; }
    }
}
        