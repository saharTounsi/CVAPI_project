using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CVAPI.Schemas;


namespace CVAPI.Models {
    public class CVExport:CVExportSchema {
        //Props
       

        //Relationships
        [JsonIgnore] [ForeignKey("id")]
        public User user { get; set; } 
        
        [JsonIgnore]
        public CVVersion version { get; set; }
    }
}
