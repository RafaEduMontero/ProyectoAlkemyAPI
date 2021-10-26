using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        #region Objects and Constructor
        public readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        #endregion

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var userlist = await _userServices.GetAll();

            return Ok(userlist);
        }

        [Authorize]
        [HttpPatch]
        public async Task<ActionResult<Result>> Update([FromForm] UserUpdateDTO userUpdateDTO)
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _userServices.Update(userUpdateDTO, token);
            return response.HasErrors
                ? BadRequest(response.Messages)
                : Ok(response);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<Result>> Delete()
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _userServices.Delete(token);
            return response;
        }
    }
}