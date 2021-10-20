﻿using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using OngProject.Core.Helper.Pagination;
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
    public class MemberServices : IMemberServices 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _ImageService;
        private readonly IUriService _uriService;
        public MemberServices(IUnitOfWork unitOfWork, IImageService ImageService, IUriService uriService)
        {
            _unitOfWork = unitOfWork;
            _ImageService = ImageService;
            _uriService = uriService;
        }

        public async Task<Result> Delete(int id)
        {
            return await _unitOfWork.MemberRepository.Delete(id);
        }

        public async Task<IEnumerable<MembersDTO>> GetAll()
        {
           
            var mapper = new EntityMapper();
            var Member = await _unitOfWork.MemberRepository.GetAll();
    
            var MemberDTO = Member.Select(x => mapper.FromMembersToMembersDto(x));
            return MemberDTO;
        }

        public async Task<PaginationDTO<MembersDTO>> GetByPage(string route ,int page)
        {
            if (page <= 0) page = 1;
            int elementsByPage = 10; // Condición exigida por negocio
            var m = await _unitOfWork.MemberRepository.GetPageAsync(x=> x.Name, elementsByPage, page);
            var items= m.ToList();
            var mapper = new EntityMapper();
            var itemsList = items.Select(x => mapper.FromMembersToMembersDto(x)).ToList();
            var totalItems = await _unitOfWork.MemberRepository.CountAsync();
            var totalpages = (int)Math.Ceiling((double)totalItems / elementsByPage);

            var response = new PaginationDTO<MembersDTO>()
            {
                CurrentPage = page,
                TotalItems = totalItems,
                TotalPages = totalpages,
                PrevPage = page > 1 ? _uriService.GetPage(route, page - 1) : null,
                NextPage = page < totalpages ? _uriService.GetPage(route, page + 1) : null,
                Items = itemsList
            };

            return response;
        }

        public async Task<Member> Insert(MembersInsertarDTO membersInsertarDTO)
        {
           
            var mapper = new EntityMapper();
            var member = mapper.FromMembersDTOtoMember(membersInsertarDTO);
            string uniqueName = "Member_" + DateTime.Now.ToString().Replace(",", "").Replace("/", "").Replace(" ", "");
            var urlImage = await _ImageService.Save(uniqueName + membersInsertarDTO.Image.FileName, membersInsertarDTO.Image);
            member.Image=urlImage.ToString();       
            await _unitOfWork.MemberRepository.Insert(member);
            await _unitOfWork.SaveChangesAsync();
            return member;
        }



    }
}
