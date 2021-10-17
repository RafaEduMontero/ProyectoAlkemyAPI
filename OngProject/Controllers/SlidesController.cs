using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Interfaces.IServices;
using OngProject.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("/slides")]
    [ApiController]
    public class SlidesController : ControllerBase
    {
        private readonly ISlidesServices _slidesServices;

        public SlidesController(ISlidesServices slidesServices)
        {
            _slidesServices = slidesServices;
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IEnumerable<SlidesDTO>> Get()
        {
            return await _slidesServices.GetAll();
        }

        [Route("/slides/public")]
        [HttpGet]
        public async Task<List<SlidesPublicDTO>> GetPublic()
        {
            return await _slidesServices.GetAllPublic();
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!_slidesServices.EntityExist(id)) return NotFound();

            var slide = await _slidesServices.GetById(id);
            
            return Ok(slide);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<Result>> Insert(SlidesCreateDTO slidesCreateDTO)
        {
            var response = await _slidesServices.Insert(slidesCreateDTO);
            return response.HasErrors
                ? BadRequest(response.Messages)
                : Ok(response);

        }
    }
}