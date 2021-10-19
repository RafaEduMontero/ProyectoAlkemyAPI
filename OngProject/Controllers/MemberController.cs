using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.DTOs;
using OngProject.Core.Helper.Pagination;
using OngProject.Core.Helper.Pagination.Extensions;
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
        #region Objects and Constructor
        private readonly IMemberServices _memberServices;
        public MemberController(IMemberServices memberServices)
        {
            _memberServices = memberServices;
        }
        #endregion

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IEnumerable<MembersDTO>> GetAll()
        {
            return await _memberServices.GetAll();
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<PaginationDTO<MembersDTO>>> GetPages([FromQuery] int page)
        {
            var response = await _memberServices.GetByPage(page);

            return Ok(this.AddPageLinks(nameof(this.GetPages), page, response));
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
    }
}
