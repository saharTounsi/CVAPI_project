using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CVAPI.Models;
using Microsoft.AspNetCore.Identity;


namespace CVAPI.Schemas {
    public class UserUpdateSchema {

        [Required]
        public string name {get;set;}

        [Required]
        public string password { get; set; }

    }
}