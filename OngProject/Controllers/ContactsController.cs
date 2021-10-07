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
    [Route("/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsServices _contactsServices;

        public ContactsController(IContactsServices contactsServices)
        {
            _contactsServices = contactsServices;
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IEnumerable<ContactsDTO>> Get()
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
    }
}
