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
     [Route("/member")]
    [ApiController]
    public class MemberController : Controller
    {
         private readonly IMemberServices _memberServices;

        public MemberController(IMemberServices memberServices)
        {
            _memberServices = memberServices;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
         public async Task<IEnumerable<MembersDTO>> Get()
        {
            return await _memberServices.GetAll();
        }
    }    
}
