using System.Collections.Generic;
using System.Net.Mail;

namespace OngProject.Core.Helper.Pagination
{
    public interface ILinkedResource
    {
        public IDictionary<LinkedResourceType, LinkedResource> Links { get; set; }
    }
}