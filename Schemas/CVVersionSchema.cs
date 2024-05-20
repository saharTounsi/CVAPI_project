using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVAPI.Schemas {
    public class CVVersionSchema {
         [Key] [Required] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }

        [Required]
        public string cvId { get; set; }

        [Required]
        public DateTime datetime { get; set; }

        [Required]
        public string path { get; set; }
    }
}
        