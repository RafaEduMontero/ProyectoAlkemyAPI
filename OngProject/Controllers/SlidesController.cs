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
        #region Object and Contructor
        private readonly ISlidesServices _slidesServices;

        public SlidesController(ISlidesServices slidesServices)
        {
            _slidesServices = slidesServices;
        }
        #endregion

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
        public async Task<ActionResult<Result>> Insert([FromForm]SlidesCreateDTO slidesCreateDTO)
        {
            var response = await _slidesServices.Insert(slidesCreateDTO);
            return response.HasErrors
                ? BadRequest(response.Messages)
                : Ok(response);

        }

        #region 
        /// <summary>
        /// Este metodo permite modificar un Slide de la base de datos
        /// </summary>
        /// <remarks>Ejemplo: 
        /// {
        /// "imageUrl": "url modificada de la imagen",
        /// "text": "slide1 a modificar",
        /// "order": 14,
        /// "organizationId": 2
        /// }
        /// 
        /// </remarks>
        /// <param name="slidesDTO"></param>
        /// <returns>
        /// <para>
        /// Los nuevos datos del Slide + un mensaje de modificacion correcta
        /// </para>
        /// <para>Caso contrario, BadRequest para cuando existe un slide con el mismo Id</para>
        /// </returns> 
        #endregion
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> UpdateSlide([FromForm] SlidesDTO slidesDTO)
        {
            var request = await _slidesServices.Update(slidesDTO);

            return request.HasErrors
                ? BadRequest(request.Messages)
                : Ok(request);
        }


    }
}