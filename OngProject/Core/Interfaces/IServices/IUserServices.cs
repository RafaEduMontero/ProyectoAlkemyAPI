using Microsoft.AspNetCore.Mvc;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace OngProject.Core.Interfaces.IServices
{
    public interface IUserServices
    {
        Task<Result> Register(UserDTO userDTO);
        Task<UserDTO> GetByEmail(string email);
    }
}