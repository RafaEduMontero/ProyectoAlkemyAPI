﻿using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Interfaces.IServices;
using OngProject.Core.Mapper;
using OngProject.Infrastructure.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public async Task<NewCommentsDTO> Insert(NewCommentsDTO newCommentDTO)
        {
            if(await _unitOfWork.UsersRepository.GetById(newCommentDTO.UserId) == null) return null;
            if(await _unitOfWork.NewsRepository.GetById(newCommentDTO.NewId) == null) return null;
            var newComment = new EntityMapper().FromNewCommentsDtoToComments(newCommentDTO);
            await _unitOfWork.CommentsRepository.Insert(newComment);

            await _unitOfWork.SaveChangesAsync();
            return newCommentDTO;
        }

        public async Task<Result> Delete(int id)
        {
            var response = await _unitOfWork.CommentsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return response;
        }

        public async Task<bool> ValidateCreatorOrAdmin(ClaimsPrincipal user, int id)
        {
            var userId = user.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            var comment =  await _unitOfWork.CommentsRepository.GetById(id);
            if(comment.UserId.Equals(int.Parse(userId)) || user.IsInRole("Administrator")) return true;
            return false;
        }
    }
}
