using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Entities
{
    public class Testimonials : EntityBase
    {
        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string name { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string image { get; set; }

        [Column(TypeName = "VARCHAR(65535)")]
        [MaxLength(65535)]
        public int content { get; set; }
    }
}
