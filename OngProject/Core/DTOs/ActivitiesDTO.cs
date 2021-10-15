using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.DTOs
{
    public class ActivitiesDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
         public string Image { get; set; }
    }
}