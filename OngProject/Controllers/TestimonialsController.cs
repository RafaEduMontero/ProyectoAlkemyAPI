using Microsoft.AspNetCore.Mvc;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    public class TestimonialsController : Controller
    {
        private readonly ITestimonialsServices _testimonialsServices;
        public TestimonialsController(ITestimonialsServices testimonialsServices)
        {
            _testimonialsServices = testimonialsServices;
        }
        [HttpPost]
        public async Task<ActionResult<Result>> Insert(TestimonialsCreateDTO testimonialDTO)
        {
            var response = await _testimonialsServices.Insert(testimonialDTO);

            return response.HasErrors

                ? BadRequest(response.Messages)

                : Ok(response);
        }
    }
}
