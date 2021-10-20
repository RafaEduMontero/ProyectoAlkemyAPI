using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using OngProject.Core.Interfaces.IServices;
using OngProject.Core.Interfaces.IServices.AWS;
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
        private readonly IImageService _imageServices;
        public CategoriesServices(IUnitOfWork unitOfWork, IImageService imageServices)
        {
            _unitOfWork = unitOfWork;
            _imageServices = imageServices;
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

        public async Task<Category> Post(CategoryDTO categoryDTO)
        {
            var mapper = new EntityMapper();
            var category = mapper.FromCategoryCreateDtoToCategory(categoryDTO);
            await _unitOfWork.CategoryRepository.Insert(category);
            await _unitOfWork.SaveChangesAsync();
            return category;

        }

        public async Task<Result> Update(int id, UpdateCategoryDTO updateCategoryDTO)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);
            if (category == null) return new Result().NotFound();

            if (updateCategoryDTO.Image != null) category.Image = await _imageServices.Save(category.Image, updateCategoryDTO.Image);
            if (updateCategoryDTO.Description != null) category.Description = updateCategoryDTO.Description;
            if (updateCategoryDTO.Name != null) category.Name = updateCategoryDTO.Name;

            await _unitOfWork.CategoryRepository.Update(category);

            _unitOfWork.SaveChanges();

            return new Result().Success($"El item se ha modificado correctamente!!" +
                $" {category.Name}");
        }
    }
}
