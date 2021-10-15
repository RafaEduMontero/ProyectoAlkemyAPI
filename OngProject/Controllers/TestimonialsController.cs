using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Route("testimonials")]
    [ApiController]
    public class TestimonialsController : Controller
    {
        private readonly ITestimonialsServices _testimonialsServices;
        public TestimonialsController(ITestimonialsServices testimonialsServices)
        {
            _testimonialsServices = testimonialsServices;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<Result>> Insert([FromForm]TestimonialsCreateDTO testimonialDTO)
        {
            var response = await _testimonialsServices.Insert(testimonialDTO);

            return response.HasErrors

                ? BadRequest(response.Messages)

                : Ok(response);
        }
    }
}
