using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OngProject.Core.Helper.Pagination
{
    public class PaginationDTO<T> : ILinkedResource
    {
        public int CurrentPage { get; init; }    
        public int TotalItems { get; init; }    
        public int TotalPages { get; init; }    
        public List<T> Items { get; init; }
        public IDictionary<LinkedResourceType, LinkedResource> Links { get; set; }
    }
}
