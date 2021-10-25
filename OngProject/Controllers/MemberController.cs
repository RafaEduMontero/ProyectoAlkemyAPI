using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Helper.Pagination;
using OngProject.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        #region Objects and Constructor
        private readonly IMemberServices _memberServices;
        public MemberController(IMemberServices memberServices)
        {
            _memberServices = memberServices;
        }
        #endregion

        [Authorize(Roles = "Administrator")]
        [HttpGet()]
        public async Task<ActionResult<PaginationDTO<MembersDTO>>> GetAll([FromQuery] int page)
        {
            string route = Request.Path.Value.ToString();
            var response = await _memberServices.GetByPage(route, page);

            return Ok(response);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MembersInsertarDTO membersDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            var response = await _memberServices.Insert(membersDTO);
            return Ok(response);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {

            return Ok(await _memberServices.Delete(id));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public async Task<ActionResult<Result>> Update ([FromForm] MemberUpdateDTO memberUpdateDTO)
        {
            var request= await _memberServices.Update(memberUpdateDTO);
            return request.HasErrors
                ? BadRequest(request.Messages):Ok(request);

        }
    }    
}
