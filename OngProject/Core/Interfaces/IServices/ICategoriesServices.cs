using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.DTOs;

namespace OngProject.Core.Interfaces.IServices
{
    public interface ICategoriesServices
    {

        Task<CategoryDTO> GetById(int id);
         bool EntityExist(int id);
    }
}
