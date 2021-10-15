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
    [Route("/category")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesServices _CategoriesServices;

        public CategoryController(ICategoriesServices CategoriesServices)
        {
            _CategoriesServices = CategoriesServices;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryNameDTO>>> Get()
        {
            var categorias= await _CategoriesServices.GetAll();
            var cat= (from Name in categorias select 
             Name);
           return Ok(cat);
            
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!_CategoriesServices.EntityExist(id)) return NotFound();
            var category = await _CategoriesServices.GetById(id);
            return Ok(category);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
       public async Task<IActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            var response = await _CategoriesServices.Post(categoryDTO);
            return CreatedAtAction("POST", response);
        }
        
    }
}
