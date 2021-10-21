using OngProject.Common;
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

        public async Task<Result> Delete(int id)
        {
            if (!_unitOfWork.CategoryRepository.EntityExists(id)) return new Result().NotFound();

            await _unitOfWork.CategoryRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return new Result().Success($"Se ha borrado la categoria correctamente");
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

        public async Task<Category> Post(CategoryDTO categoryDTO)
        {
            var mapper = new EntityMapper();
            var category = mapper.FromCategoryCreateDtoToCategory(categoryDTO);
            await _unitOfWork.CategoryRepository.Insert(category);
            await _unitOfWork.SaveChangesAsync();
            return category;

        }
    }
}
