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
    public class MemberServices : IMemberServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly  IImageService _ImageService;
        public MemberServices(IUnitOfWork unitOfWork, IImageService ImageService)
        {
            _unitOfWork = unitOfWork;
            _ImageService = ImageService;

        }

        public async Task<IEnumerable<MembersDTO>> GetAll()
        {
           
            var mapper = new EntityMapper();
            var Member = await _unitOfWork.MemberRepository.GetAll();
    
            var MemberDTO = Member.Select(x => mapper.FromMembersToMembersDto(x));
            return MemberDTO;
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
