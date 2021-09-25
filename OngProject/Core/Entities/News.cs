using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Core.Entities
{
    public class News : EntityBase
    {
        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string name { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        [MaxLength(255)]
        public string content { get; set; }
        
        [Column(TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string image { get; set; }
        
       
    }
}
