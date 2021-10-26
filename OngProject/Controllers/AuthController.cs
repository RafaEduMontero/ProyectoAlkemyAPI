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
using OngProject.Core.DTOs.UserDTOs;

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
        public AuthController(IUserServices _userServices, JwtHelper _JwtHelper)
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
            try
            {
                if (!ModelState.IsValid) return BadRequest("Datos no validos");
                var token = Request.Headers["Authorization"];
                var userId = _userServices.GetUserId(token);
                var user = await _userServices.GetById(userId);
                return Ok(user);

            }
            catch (Result result)
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] UserInsertDTO userInsertDTO)
        {
            try
            {
                if (ModelState.IsValid)
                    if (await _userServices.GetByEmail(userInsertDTO.Email) == null)
                        await _userServices.Register(userInsertDTO);
                    else
                        return BadRequest("Este correo ya estaba en uso");
                else
                    return BadRequest("Datos invalidos, verifique los campos ingresados");
                
                return Ok(new
                {
                    Status = "Operacion exitosa! Usuario creado con exito!",
                    Token = (OkObjectResult)await Login(new LoginDTO
                    {
                        Email = userInsertDTO.Email,
                        Password = userInsertDTO.Password
                    }
                    )
                });
            }
            catch (Result result)
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromForm] LoginDTO loginDTO)
        {
            try
            {
                var userSaved = await _userServices.GetByEmail(loginDTO.Email);

                if (userSaved == null)
                {
                    return BadRequest("No se ha encontrado un usuario con este correo");
                }

                var passwordVerification = Encrypt.Verify(loginDTO.Password, userSaved.Password);
                if (!passwordVerification)
                {
                    return StatusCode(401, "Credenciales no validas");
                }

                var token = _JwtHelper.GenerateJwtToken(userSaved);

                return Ok(new { token });
            }
            catch (Result result)
            {
                return BadRequest(result.Message);
            }
        }
    }
}
