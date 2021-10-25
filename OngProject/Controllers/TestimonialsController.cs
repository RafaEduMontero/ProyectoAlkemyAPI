using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Helper.Pagination;
using OngProject.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialsServices _testimonialsServices;
        public TestimonialsController(ITestimonialsServices testimonialsServices)
        {
            _testimonialsServices = testimonialsServices;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginationDTO<TestimonialsCreateDTO>>> GetAll([FromQuery] int page, int? sizePage)
        {
            string route = Request.Path.Value.ToString();
            var response = await _testimonialsServices.GetByPage(route, page, sizePage);

            return Ok(response);
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

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            var request = await _testimonialsServices.Delete(id);

            return request.HasErrors
                ? BadRequest(request.Messages)
                : Ok(request);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Update(int id, [FromForm] TestimonialsCreateDTO testimonialsCreateDTO)
        {
            var request = await _testimonialsServices.Update(id, testimonialsCreateDTO);

            return request.HasErrors
                ? BadRequest(request.Messages)
                : Ok(request);
        }
    }
}
