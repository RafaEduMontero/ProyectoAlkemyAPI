using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OngProject.Core.Entities
{
    public class Organizations : EntityBase
    {
        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string name { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string image { get; set; }
        
        [Column(TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string address { get; set; }
        
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
        public string welcomeText { get; set; }
        
        [Column(TypeName = "TEXT")]
        [MaxLength(2000)]
        public string aboutUsText { get; set; }
    }
}

