using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.DTOs
{
    public class CategoryDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
         public string Image { get; set; }
    }
}
