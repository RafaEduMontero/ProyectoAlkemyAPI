using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces.IServices;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            if (!_newsServices.EntityExists(id))
                return NotFound();
            var news = await _newsServices.GetById(id);
            return Ok(news);
        }
    }
}
