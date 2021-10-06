using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Object and Constructor
        private readonly IUserServices userServices;
        public AuthController(IUserServices userServices)
        {
            this.userServices = userServices;
        } 
        #endregion

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserDTO userDTO)
        {
            
            var userSaved = await userServices.GetByEmail(userDTO.Email);
            
            if (userSaved==null)
            {
                return BadRequest("No se ha encontrado un usuario con este correo...");
            }

            var passwordVerification = Encrypt.Verify(userDTO.Password, userSaved.Password);
            if (!passwordVerification)
            {
                return StatusCode(401, "Credenciales no validas");
            }

            var token = new JwtSecurityToken();
            return Ok(token);
        }
    }
}
