using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.DTOs
{
    public class NewCommentsDTO
    {
        [Required]
        public string Body { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int NewId { get; set; }
    }
}