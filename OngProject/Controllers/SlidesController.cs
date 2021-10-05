using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IEnumerable<SlidesDTO>> Get()
        {
            return await _slidesServices.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!_slidesServices.EntityExist(id)) return NotFound();
            var slide = await _slidesServices.GetAll();
            return Ok(slide);
        }
    }
}