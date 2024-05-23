using System.ComponentModel.DataAnnotations;


namespace CVAPI.Schemas {
    public class AuthData {
        
        [Required]
        public string userId { get; set; }

        public string sessionId { get; set; }
    }
}
        