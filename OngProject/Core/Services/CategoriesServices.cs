using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using OngProject.Core.Interfaces.IServices;
using OngProject.Core.Mapper;
using OngProject.Infrastructure.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoriesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool EntityExist(int id)
        {
            return _unitOfWork.CategoryRepository.EntityExists(id);
        }

        public async Task<IEnumerable<CategoryNameDTO>> GetAll()
        {
            var mapper = new EntityMapper();
            var CategoryList = await _unitOfWork.CategoryRepository.GetAll();
            var CategoryDTOList = CategoryList.Select(x => mapper.FromCategoryToCategoryNameDto(x)).ToList();
            return CategoryDTOList;
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var mapper = new EntityMapper();
            var category = await _unitOfWork.CategoryRepository.GetById(id);
            var categoryDTO = mapper.FromCategoryToCategoryDto(category);
            return categoryDTO;
        }
       
    }
}
