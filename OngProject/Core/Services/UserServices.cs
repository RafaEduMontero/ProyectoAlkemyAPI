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
        //private readonly UserRepository _userRepository;
        public UserServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #endregion
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
            var user = await _unitOfWork.UsersRepository.FindByCondition(x => x.Email == email);
            return user;
        }
    }
}