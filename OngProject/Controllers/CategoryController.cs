using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Common;
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
        [HttpGet("/GetAll")]
        public async Task<ActionResult<IEnumerable<CategoryNameDTO>>> GetAll([FromQuery] int page)
        {

            string route = Request.Path.Value.ToString();
            var response = await _CategoriesServices.GetByPage(route, page);

            return Ok(response);
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

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Update(int id, [FromForm] UpdateCategoryDTO updateCategoryDTO)
        {
            var request = await _CategoriesServices.Update(id, updateCategoryDTO);
            
            return request.HasErrors
                ? BadRequest(request.Messages)
                : Ok(request);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            var request = await _CategoriesServices.Delete(id);

            return request.HasErrors
                ? BadRequest(request.Messages)
                : Ok(request);
        }

    }
}
