using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Entities
{
    public class Contacts
    {
        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string name { get; set; }

        [Column(TypeName = "INTEGER")]
        [MaxLength(20)]
        public int phone { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(320)")]
        [MaxLength(320)]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        [MaxLength(500)]
        public string message { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime deletedAt { get; set; }

    }
}

