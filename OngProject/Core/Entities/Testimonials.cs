using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Entities
{
    public class Testimonials : EntityBase
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Content { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
