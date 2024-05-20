using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVAPI.Schemas {
    public class CVExportSchema {
        [Key] [Required] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public string id { get; set; }

        public string name { get;set; }

       public string exporterId { get; set; }
          [Required]
        public string versionId { get; set; }
      

        [Required]
        public DateTime datetime { get; set; }

    }
    
}