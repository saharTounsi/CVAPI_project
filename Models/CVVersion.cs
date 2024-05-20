using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CVAPI.Schemas;


namespace CVAPI.Models {
    public class CVVersion:CVVersionSchema {
        //Props
       

        //Relationships
        [JsonIgnore] [ForeignKey("id")]
        public CV cv { get; set; }
        
        [JsonIgnore]
        public List<CVExport> cvExports { get; set; }
    }
}
