using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.DTOs
{
    public class ContactsDTO
    {
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
