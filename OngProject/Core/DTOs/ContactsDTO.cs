using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.DTOs
{
    public class ContactsDTO
    {

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }


        [MaxLength(20)]
        public int Phone { get; set; }

        [Required]
        [MaxLength(320)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(500)]
        public string Message { get; set; }
    }
}
