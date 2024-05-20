using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVAPI.Schemas {
    public class CVSchema {
        
        [Key] [Required] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }

        [Required]
        public string? name {get;set;}

        [Required]
        public DateTime datetime { get; set; }

        public string? status { get; set; }
        
    }
}
        