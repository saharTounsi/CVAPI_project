﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CVAPI.Schemas;


namespace CVAPI.Models {
    public class CV {
        //Props
        [Key] [Required] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }

        [Required]
        public string userId { get; set; }

        [Required]
        public string? name {get;set;}

        [Required]
        public DateTime datetime {get;set;}=DateTime.UtcNow;

        public string? status { get; set; }

        [Required]
        public string currentVersionId { get; set; }
        
        //Relationships
        [JsonIgnore] [ForeignKey("id")]
        public User user { get; set; }
        
        [JsonIgnore]
        public List<CVModif> cvModifs { get; set; }

        [JsonIgnore]
        public List<CVVersion> versions { get; set; }
        //public List<CVExport> cvExports { get; set; }
    }
}
