using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.DTOs
{
    public class OrganizationsDTO
    {
        public string name { get; set; }
        public string image { get; set; }
        public string address { get; set; }
        public int phone { get; set; }
        public string email { get; set; }
        public string welcomeText { get; set; }
        public string aboutUsText { get; set; }
    }
}
