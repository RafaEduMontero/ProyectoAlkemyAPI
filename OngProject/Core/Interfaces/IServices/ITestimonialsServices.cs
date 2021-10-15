using OngProject.Common;
using OngProject.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces.IServices
{
    public interface ITestimonialsServices
    {
        Task<Result> Insert(TestimonialsCreateDTO testimonialsDTO);
    }
}
