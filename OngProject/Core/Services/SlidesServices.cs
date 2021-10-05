using OngProject.Core.DTOs;
using OngProject.Core.Interfaces.IServices;
using OngProject.Core.Mapper;
using OngProject.Infrastructure.Repositories.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Services
{
    public class SlidesServices : ISlidesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public SlidesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<SlidesDTO>> GetAll()
        {
            var mapper = new EntityMapper();
            var slideList = await _unitOfWork.SlidesRepository.GetAll();
            var slideDTOList = slideList.Select(x => mapper.FromSlideToSlideDto(x)).ToList();
            return slideDTOList;
        }
    }
}
