using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CVAPI.Schemas {
    public class CVVersionSchema:CVData {
        [Key] [Required] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }

        [Required]
        public string cvId { get; set; }

        [Required]
        public DateTime datetime { get; set; }
    }
}
        