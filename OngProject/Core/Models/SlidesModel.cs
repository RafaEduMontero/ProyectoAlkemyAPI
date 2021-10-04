using OngProject.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Core.Models
{
    public class SlidesModel : EntityBase
    {
        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string ImageUrl { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(4000)")]
        [MaxLength(4000)]
        public string Text { get; set; }

        public int Order { get; set; }

        public int OrganizationId { get; set; }

    }
}
