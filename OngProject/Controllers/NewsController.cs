using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Helper.Pagination;
using OngProject.Core.Interfaces.IServices;
using System;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {

        private readonly INewsServices _newsServices;

        public NewsController(INewsServices newsServices)
        {
            _newsServices = newsServices;
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (!_newsServices.EntityExists(id))
                    return NotFound();
                var news = await _newsServices.GetById(id);
                return Ok(news);
            }
            catch(Exception)
            {
                return NotFound();
            }
            
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginationDTO<NewsDTO>>> GetAll([FromQuery] int page)
        {
            try
            {
                string route = Request.Path.Value.ToString();
                var response = await _newsServices.GetByPage(route, page);

                return Ok(response);
            }
            catch(Exception)
            {
                return NotFound();
            }
            
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm]NewsInsertDTO newsInsertDTO)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                Result response = await _newsServices.Insert(newsInsertDTO);
                return (response != null) ? Ok("Novedad creada con exito") : BadRequest("Ocurrio un error al crear la Novedad");
            }
            catch(Result ex)
            {
                return BadRequest(ex.Message);
            }
             
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            try
            {
                Result request = await _newsServices.Delete(id);
                return request.HasErrors ? BadRequest(request.Messages) : Ok(request);
            }
            catch(Result ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Update([FromForm] NewsUpdateDTO newsUpdateDTO, int id)
        {
            try
            {
                Result request = await _newsServices.Update(newsUpdateDTO, id);
                return request.HasErrors ? BadRequest(request.Messages) : Ok(request);
            }
            catch(Result ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
