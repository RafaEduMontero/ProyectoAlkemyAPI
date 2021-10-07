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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryNameDTO>>> Get()
        {
            var categorias= await _CategoriesServices.GetAll();
            var cat= (from Name in categorias select 
             Name);
           return Ok(cat);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!_CategoriesServices.EntityExist(id)) return NotFound();
            var category = await _CategoriesServices.GetById(id);
            return Ok(category);
        }
        
       
        
    }
}
