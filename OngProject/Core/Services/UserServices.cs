using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using OngProject.Core.Interfaces.IServices;
using OngProject.Core.Mapper;
using OngProject.Infrastructure.Repositories;
using OngProject.Infrastructure.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Services
{
    public class UserServices : IUserServices
    {
        #region Object and Constructor
        private readonly IUnitOfWork _unitOfWork;
        public UserServices(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
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
            var newUser = new EntityMapper().FromUserDtoToUser(userDTO);

            newUser.RoleId = 2;
            newUser.Password = Encrypt.GetSHA256(newUser.Password);

            await _unitOfWork.UsersRepository.Insert(newUser);
            
            _unitOfWork.SaveChanges();

            return new Result().Success($"Se ha agregado correctamene al usuario {newUser.FirstName}");
        }
        public async Task<User> GetByEmail(string email)
        {
            var user = await _unitOfWork.UsersRepository.GetByEmail(email);
            
            return user;
        }
    }
}