using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using OngProject.Core.Interfaces.IServices;
using OngProject.Core.Mapper;
using OngProject.Infrastructure.Repositories.IRepository;
using System.Threading.Tasks;

namespace OngProject.Core.Services
{
    public class NewsServices : INewsServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
