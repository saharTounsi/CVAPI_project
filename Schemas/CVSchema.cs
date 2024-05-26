using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CVAPI.Models;

namespace CVAPI.Schemas {
    public class CVSchema {
        
        public CVSchema(){} 
        public CVSchema(CV cv,CVVersion? version){
            this.id=cv.id;
            this.userId=cv.userId;
            this.name=cv.name;
            this.datetime=cv.datetime;
            if(version!=null){
                this.data=new CVData(){
                    fileName=version.fileName,
                    profileName=version.profileName,
                    profileEmail=version.profileEmail,
                    profileTel=version.profileTel,
                    profileSkills=version.profileSkills,
                    profileExperience=version.profileExperience,
                };
            }
        }

        [Key] [Required] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }

        [Required]
        public string userId { get; set; }

        [Required]
        public string? name {get;set;}

        [Required]
        public DateTime datetime { get; set; }

        public string? status { get; set; }

        public CVData? data {get;set;}
        
    }
}
        