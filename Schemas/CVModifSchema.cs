using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVAPI.Schemas {
    public class CVModifSchema {
        [Key] [Required] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }

        [Required]
        public string cvId { get; set; }

        [Required]
        public string editorId { get; set; }

        public DateTime datetime { get; set; }

    }
}