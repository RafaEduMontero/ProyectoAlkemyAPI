using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.DTOs;
using OngProject.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsServices _commentsServices;
        public CommentsController(ICommentsServices commentsServices)
        {
            _commentsServices = commentsServices;
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IEnumerable<CommentsDTO>> Get()
        {
            return await _commentsServices.GetAll();
        }
        [HttpGet("/news/{id}/comments")]
        public async Task<IActionResult> Get(int id)
        {
            if (!_commentsServices.EntityExists(id)) return NotFound();
            var contact = await _commentsServices.GetById(id);
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] NewCommentsDTO newCommentsDTO)
        {
            var request = await _commentsServices.Insert(newCommentsDTO);

            return (request != null) ? Ok() : BadRequest("No se ha podido ingresar el comentario");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_commentsServices.EntityExists(id))
            {
                if (! await _commentsServices.ValidateCreatorOrAdmin(User, id)) return Forbid();
                return Ok(await _commentsServices.Delete(id));
                
            }
            return Ok(await _commentsServices.Delete(id));
        }
    }
}
