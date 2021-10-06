using OngProject.Core.DTOs;
using OngProject.Core.Interfaces.IServices;
using OngProject.Core.Mapper;
using OngProject.Infrastructure.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Services
{
    public class CommentsServices : ICommentsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool EntityExists(int id)
        {
            return _unitOfWork.CommentsRepository.EntityExists(id);
        }

        public async Task<IEnumerable<CommentsDTO>> GetAll()
        {
            var mapper = new EntityMapper();
            var commentsList = await _unitOfWork.CommentsRepository.GetAll();
            var commentsListDTO = commentsList.OrderBy(y => y.CreatedAt).Select(x => mapper.FromCommentsToCommentsDto(x)).ToList();
            return commentsListDTO;
        }

        public async Task<IEnumerable<CommentsDTO>> GetById(int id)
        {
            var mapper = new EntityMapper();
            var commentsList = await _unitOfWork.CommentsRepository.GetAll();
            var commentsListDTO = commentsList.Where(y => y.NewId == id).Select(x => mapper.FromCommentsToCommentsDto(x)).ToList();
            return commentsListDTO;
        }
    }
}
