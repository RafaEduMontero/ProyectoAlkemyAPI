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
        public async Task<ActionResult<PaginationDTO<TestimonialsCreateDTO>>> GetAll([FromQuery] int page)
        {
            try
            {
                string route = Request.Path.Value.ToString();
                var response = await _testimonialsServices.GetByPage(route, page);

                return Ok(response);
            }
            catch(Result result)
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<Result>> Insert([FromForm]TestimonialsCreateDTO testimonialDTO)
        {
            try
            {
                var response = await _testimonialsServices.Insert(testimonialDTO);

                return Ok(response);
            }
            catch(Result result){
                return BadRequest(result.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            try
            {
                var response = await _testimonialsServices.Delete(id);

                return Ok(response);
            }
            catch(Result result)
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Update(int id, [FromForm] TestimonialsUpdateDTO testimonialsUpdateDTO)
        {try
            {
                var response = await _testimonialsServices.Update(id, testimonialsUpdateDTO);
                return Ok(response);
            }
            catch (Result result) { return BadRequest(result.Message);}
        }
    }
}
