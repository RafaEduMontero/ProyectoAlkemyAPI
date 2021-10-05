using Microsoft.AspNetCore.Mvc;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace OngProject.Core.Interfaces.IServices
{
    public class IUserServices
    {
        Task<User> GetByEmail(UserDTO userDTO);
    }
}