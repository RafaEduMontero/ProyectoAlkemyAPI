using Microsoft.AspNetCore.Mvc;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using OngProject.Core.Interfaces.IServices;
using OngProject.Core.Interfaces.IServices.AWS;
using OngProject.Core.Mapper;
using OngProject.Infrastructure.Repositories.IRepository;
using System.Threading.Tasks;

namespace OngProject.Core.Services
{
    public class NewsServices : INewsServices
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IImageService _imageServices;
        public NewsServices(IUnitOfWork unitOfWork, IImageService imageServices)
        {
            _unitOfWork = unitOfWork;
            _imageServices = imageServices;
        }

        public async Task<NewsDTO> GetById(int id)
        {
            var maper = new EntityMapper();
            var news = await _unitOfWork.NewsRepository.GetById(id);
            var newsDTO = maper.FromNewsToNewsDTO(news);
           
            return newsDTO;

        }
        public bool EntityExists(int id)
        {
            return _unitOfWork.NewsRepository.EntityExists(id);
        }

        public async Task<News> Insert(NewsDTO newsDTO)
        {
            var mapper = new EntityMapper();
            var news = mapper.FromNewsDTOtoNews(newsDTO);
            await _unitOfWork.NewsRepository.Insert(news);
            await _unitOfWork.SaveChangesAsync();
            return news;


        }

        public async Task<Result> Delete(int id)
        {
            var news = await _unitOfWork.NewsRepository.GetById(id);
            if (news == null)
            {
                return new Result().NotFound();
            }
            var url = news.Image;
            var result = await _unitOfWork.NewsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            if (result != null)
            {
                if(url != null)
                {
                    await _imageServices.Delete(url);
                }
                
                return new Result().Success("Novedad eliminada con exito");
            }
            return new Result().Fail("Ocurrio un error al eliminar la novedad");
        }

        public async Task<Result> Update(int id, NewsUpdateDTO newsUpdateDTO)
        {
            var news = await _unitOfWork.NewsRepository.GetById(id);
            if (news == null)
            {
                return new Result().NotFound();
            }
            if (!string.IsNullOrEmpty(newsUpdateDTO.Name))
            {
                news.Name = newsUpdateDTO.Name;
            }
            if (!string.IsNullOrEmpty(newsUpdateDTO.Content))
            {
                news.Content = newsUpdateDTO.Content;
            }
            if (newsUpdateDTO.Image != null)
            {

                if (news.Image == null)
                {
                    news.Image = await _imageServices.Save(newsUpdateDTO.Image.FileName, newsUpdateDTO.Image);
                }
                else if(await _imageServices.Delete(news.Image))
                {
                    news.Image = await _imageServices.Save(newsUpdateDTO.Image.FileName, newsUpdateDTO.Image);
                }
            }
            if (newsUpdateDTO.CategoryId > 0)
            {
                if (_unitOfWork.CategoryRepository.EntityExists(newsUpdateDTO.CategoryId))
                {
                    news.CategoryId = newsUpdateDTO.CategoryId;
                }
                
            }
            await _unitOfWork.NewsRepository.Update(news);
            await _unitOfWork.SaveChangesAsync();

            var newsUpdate = await _unitOfWork.NewsRepository.GetById(id);

            if (newsUpdate.Name == news.Name
                && newsUpdate.Content == news.Content
                && newsUpdate.Image == news.Image
                && newsUpdate.CategoryId == news.CategoryId)
            {

                return new Result().Success($"{newsUpdate.Name}  ," +
                                            $"{newsUpdate.Content}  ," +
                                            $"{newsUpdate.Image}  ," +
                                            $"{newsUpdate.CategoryId}"
                );

            }
            return new Result().Fail("La Novedad no se pudo modificar");
        }
    }
}
