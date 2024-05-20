using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CVAPI.Schemas;


namespace CVAPI.Models {
    public class CVModif:CVModifSchema {
        //Props
       

        //Relationships
        [JsonIgnore] [ForeignKey("id")]
        public CV cv { get; set; }
        
        [JsonIgnore]
        public User user { get; set; }
    }
}
