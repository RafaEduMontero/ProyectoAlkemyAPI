using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Interfaces.IServices;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("/news")]
    public class NewsController : Controller
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
            if (!_newsServices.EntityExists(id))
                return NotFound();
            var news = await _newsServices.GetById(id);
            return Ok(news);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post(NewsDTO newsDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            var response = await _newsServices.Insert(newsDTO);
            return Ok(response);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            var request = await _newsServices.Delete(id);

            return request.HasErrors
                ? BadRequest(request.Messages)
                : Ok(request);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Update(int id, [FromForm] NewsUpdateDTO newsUpdateDTO)
        {
            var request = await _newsServices.Update(id, newsUpdateDTO);

            return request.HasErrors
                ? BadRequest(request.Messages)
                : Ok(request);
        }
    }
}
