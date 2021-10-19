using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace OngProject.Core.DTOs
{
    public class MemberUpdateDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required(ErrorMessage = "Escriba su nombre")]
        public string Name { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
    }
}
