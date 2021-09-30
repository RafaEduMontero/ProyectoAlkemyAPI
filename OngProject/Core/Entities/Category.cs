using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Entities
{
    public class Category : EntityBase
    {
        [Required(ErrorMessage = "Nombre de la categoria es requerido")]
        [Column(TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string name { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string description { get; set; }

        [Required]
        [Column(TypeName ="VARCHAR(255)")]
        [MaxLength(255)]
        public string image { get; set; }

    }
}
