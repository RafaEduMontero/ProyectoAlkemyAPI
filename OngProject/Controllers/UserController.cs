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
    [Route("users")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        #region Objects and Constructor
        public readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        } 
        #endregion

        [HttpGet]
        [Authorize(Roles ="Administrator")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var userlist = await _userServices.GetAll();
            
            return Ok(userlist);
        }

        [Authorize(Roles = "Administrator")]
        [Authorize(Roles = "Standard")]
        [HttpPut]
        public async Task<ActionResult<Result>> Update([FromForm]UserUpdateDTO userUpdateDTO)
        {
            var response = await _userServices.Update(userUpdateDTO);
            return response.HasErrors
                ? BadRequest(response.Messages)
                : Ok(response);
        }
    }
}