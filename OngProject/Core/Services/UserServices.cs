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
        public async Task<UserDTO> GetByEmail(string email)
        {
            var mapper = new EntityMapper();
            var user = await _unitOfWork.UsersRepository.FindByCondition(x => x.Email == email);
                
            var userDTO = mapper.FromsUserToUserDto(user);
            return userDTO;
        }
    }
}