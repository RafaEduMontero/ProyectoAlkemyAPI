using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using OngProject.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]

    public class ActivitiesController : Controller
    {
        private readonly IActivitiesServices _activitiesServices;

        public ActivitiesController(IActivitiesServices activitiesServices)
        {
            _activitiesServices = activitiesServices;
        }

        [HttpGet]
        public async Task<IEnumerable<ActivitiesDTO>> Get()
        {
            return await _activitiesServices.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(!_activitiesServices.EntityExists(id))
            {
                return NotFound();
            }
            var activity = await _activitiesServices.GetById(id);
            return Ok(activity);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]ActivitiesDTO activitiesDTO)
        {
            var instert = await _activitiesServices.Insert(activitiesDTO);

            return (instert != null) ? Ok("Actividad creada con exito") : BadRequest("Ocurrio un error al crear la actividad");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Update([FromBody] ActivitiesDTO activitiesDTO,int id)
        {
            var response = await _activitiesServices.Update(activitiesDTO,id);
            return response.HasErrors
                ? BadRequest(response.Messages)
                : Ok(response);
        }

    }
}
