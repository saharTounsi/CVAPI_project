using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CVAPI.Schemas;


namespace CVAPI.Models {
    public class User : UserSchema {
        //Props

        public User(){}
        public User(UserSignUpSchema data):base(data){

        }

        //Relationships
        [JsonIgnore]
        public List<CV> cvs { get; set; }
        
        [JsonIgnore]
        public List<CVModif> cvModifs { get; set; }

        [JsonIgnore]
        public List<CVExport> cvExports { get; set; }


        public enum Role {Employee,HR,Admin};
    }
}
