using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.DTOs
{
    public class CommentUpdateDTO
    {
        public int? UserId { get; set; }
        public int? NewId { get; set; }
        public string Body { get; set; }
    }
}
