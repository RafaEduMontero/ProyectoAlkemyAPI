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
using OngProject.Core.Mapper;
using OngProject.Core.Helper;
using OngProject.Core.Services;
using NSwag.Annotations;

namespace OngProject.Controllers
{
   
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Object and Constructor
        private readonly IUserServices _userServices;
        private readonly JwtHelper _JwtHelper;
        public AuthController(IUserServices _userServices , JwtHelper _JwtHelper)
        {
            this._userServices = _userServices;
            this._JwtHelper = _JwtHelper;
        }
        #endregion
        /// <summary>
        /// Get all data about me
        /// </summary>
        [HttpGet("me")]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid) return BadRequest();
            var token = Request.Headers["Authorization"];
            var userId =_userServices.GetUserId(token);
            var user = await _userServices.GetById(userId);
            return Ok(user);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromForm] UserDTO userDTO)
        {
            if (ModelState.IsValid){
                await _userServices.Register(userDTO);

                Response.StatusCode = StatusCodes.Status201Created;     
            }
            return Ok(new { Status = "Operacion exitosa El usuario fue creado con exito!", 
            Token = (OkObjectResult)await Login(new LoginDTO
            {
                Email = userDTO.Email,
                Password = userDTO.Password
            })
            });           
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromForm]LoginDTO loginDTO)
        {
            
            var userSaved = await _userServices.GetByEmail(loginDTO.Email);

            if (userSaved==null)
            {
                return BadRequest("No se ha encontrado un usuario con este correo...");
            }

            var passwordVerification = Encrypt.Verify(loginDTO.Password, userSaved.Password);
            if (!passwordVerification)
            {
                return StatusCode(401, "Credenciales no validas");
            }

            var token = _JwtHelper.GenerateJwtToken(userSaved);

            return Ok(new { token });
           
        }

    }
}
