using System.ComponentModel.DataAnnotations;


namespace CVAPI.Schemas {
    public class CVData {
        public string? profileName {get;set;}
        public string? profileEmail {get;set;}
        public string? profileTel {get;set;}
        public string? profileSkills {get;set;}
        public string? profileExperience {get;set;}

        [Required]
        public string fileName {get;set;}
    }
}
