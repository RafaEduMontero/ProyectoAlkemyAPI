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
        [HttpGet]
        public async Task<IEnumerable<ContactDTO>> Get()
        {
            return await _contactsServices.GetAll();
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!_contactsServices.EntityExists(id))
                return NotFound();
            
            var contact = await _contactsServices.GetById(id);
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]ContactDTO contactDTO)
        {
            var request= await _contactsServices.Insert(contactDTO);
            
            return (request != null) ? Ok() : BadRequest("No se ha podido ingresar el contacto"); 
        }
    }
}
