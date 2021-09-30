using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OngProject.Core.Entities;

namespace OngProject.Core.Entities
{
    //de esta manera esta mal 
    //para hacer el campo unique debo modificar el dbConxtex
    //[Index(nameof(email), IsUnique = true)]
    public class User : EntityBase
    {
        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string firstName { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string lastName { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(320)")]
        public string email { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(20)")]
        public string password { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string photo { get; set; }
        //Foreign Key hacia Roles
        public int roleId { get; set; }
        [ForeignKey("roleId")]
        public virtual Roll roll { get; set; }
    }
}