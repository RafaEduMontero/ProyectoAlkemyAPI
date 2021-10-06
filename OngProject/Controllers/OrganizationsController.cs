using Microsoft.AspNetCore.Mvc;
using OngProject.Core.DTOs;
using OngProject.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("/organization")]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationsServices _organizationsServices;

        public OrganizationsController(IOrganizationsServices organizationsServices)
        {
            _organizationsServices = organizationsServices;
        }

        [HttpGet("/public")]
        public async Task<OrganizationsDTO> Get()
        {
            return await _organizationsServices.Get();
        }
    }
}
