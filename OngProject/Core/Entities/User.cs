using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OngProject.Core.Entities;

namespace OngProject.Core.Entities
{
    //de esta manera esta mal 
    //para hacer el campo unique debo modificar el dbContex
    //[Index(nameof(email), IsUnique = true)]
    public class User : EntityBase
    {
        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(320)")]
        [MaxLength(320)]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(66)")]
        [MaxLength(66)]
        public string Password { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string Photo { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}