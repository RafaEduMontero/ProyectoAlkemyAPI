using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using OngProject.Core.Interfaces.IServices;
using OngProject.Core.Mapper;
using OngProject.Infrastructure.Repositories;
using OngProject.Infrastructure.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Core.Services
{
    public class UserServices : IUserServices
    {
        #region Object and Constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _emailservice;
        private IConfiguration _configuration;
        public UserServices(IUnitOfWork _unitOfWork, IMailService emailservice, IConfiguration configuration)
        {
            this._unitOfWork = _unitOfWork;
            _emailservice = emailservice;
            _configuration = configuration;
        }
        #endregion
        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var mapper = new EntityMapper();
            var request= await _unitOfWork.UsersRepository.GetAll();

            return request.Select(x => mapper.FromsUserToUserDto(x)).ToList();
        }
        public async Task<Result> Register(UserDTO userDTO)
        {

            var request= await _unitOfWork.UsersRepository.GetByEmail(userDTO.Email);
            if(request!= null){
                return new Result().Fail("Este correo ya existe");
            }
            var newUser = new EntityMapper().FromUserDtoToUser(userDTO);
            newUser.RoleId = 2;
            newUser.Password = Encrypt.GetSHA256(newUser.Password);

            await _unitOfWork.UsersRepository.Insert(newUser);

            await _emailservice.SendEmailAsync(newUser.Email,_configuration["Welcomesubject"],newUser.FirstName+" "+newUser.LastName);
            
            _unitOfWork.SaveChanges();

            return new Result().Success($"Se ha agregado correctamene al usuario {newUser.FirstName}");
        }
        public async Task<User> GetByEmail(string email)
        {
            var user = await _unitOfWork.UsersRepository.GetByEmail(email);
            
            return user;
        }

        public int GetUserId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var stringSplit = token.Split(' ');
            var Token = handler.ReadJwtToken(stringSplit[1]);
            var claims = Token.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            var id = int.Parse(claims.Value);
            return (int)id;
        }

        public async Task<UserInfoDTO> GetById(int userId)
        {
            var mapper = new EntityMapper();
            var user = await _unitOfWork.UsersRepository.GetById(userId);
            var userDTO = mapper.FromsUserInfoToUserDto(user);

            return userDTO;
        }
    }
}