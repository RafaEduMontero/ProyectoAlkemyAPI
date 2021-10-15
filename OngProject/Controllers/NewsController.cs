using Microsoft.AspNetCore.Mvc;
using OngProject.Core.DTOs;
using OngProject.Core.Interfaces.IServices;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        [HttpPost]
        public async Task<IActionResult> Post(NewsDTO newsDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            var response = await _newsServices.Insert(newsDTO);
            return Ok(response);
        }
    }
}
