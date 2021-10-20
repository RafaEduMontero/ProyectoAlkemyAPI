using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;

namespace OngProject.Core.Interfaces.IServices
{
    public interface ICategoriesServices
    {

        Task<CategoryDTO> GetById(int id);
        bool EntityExist(int id);
        Task<IEnumerable<CategoryNameDTO>> GetAll();
        Task<Category> Post(CategoryDTO categoryDTO);
        Task<Result> Update(int id, UpdateCategoryDTO updateCategoryDTO);
    }
}
