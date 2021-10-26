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
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        #region Objects and Constructor
        private readonly IContactsServices _contactsServices;
        public ContactsController(IContactsServices contactsServices)
        {
            _contactsServices = contactsServices;
        } 
        #endregion

        [Authorize(Roles = "Administrator")]
        [HttpGet("/all")]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetAll()
        {
            try
            {
                return Ok(await _contactsServices.GetAll());
            }
            catch (Result result)
            {
                return BadRequest(result.Messages);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (!_contactsServices.EntityExists(id))
                    return NotFound();

                var contact = await _contactsServices.GetById(id);
                return Ok(contact);
            }
            catch (Result result)
            {
                return BadRequest(result.Messages);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromForm]ContactInsertDTO contactInsertDTO)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Los datos no son validos.");

                var request = await _contactsServices.Insert(contactInsertDTO);
                
                if (!request.HasErrors)
                    return BadRequest("No se ha podido completar la solicitud");

                return Ok(request);
            }
            catch (Result result)
            {
                return BadRequest(result.Messages);
            }
        }
    }
}
