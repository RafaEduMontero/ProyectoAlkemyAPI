using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using OngProject.Core.Helper.S3;
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

        public async Task<Result> Delete(int id)
        {
            var respuesta = await _unitOfWork.MemberRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return respuesta;
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

        public async Task<Result> Update(MemberUpdateDTO memberUpdateDTO)
        {
        
            var consulta=await _unitOfWork.MemberRepository.GetById(memberUpdateDTO.Id);

            if(consulta==null)
            {
                return new Result().NotFound();
            }
            else if(consulta.Image != memberUpdateDTO.Image.ToString())
            {
                try
                {
                       await _ImageService.Delete(consulta.Image);                     
                }
                catch (System.Exception)
                {
                }
             
                string uniqueName = "Member_" + DateTime.Now.ToString().Replace(",", "").Replace("/", "").Replace(" ", "");
                var urlImage = await _ImageService.Save(uniqueName + memberUpdateDTO.Image.FileName, memberUpdateDTO.Image);
                consulta.Image = urlImage;
            

            }
            
            consulta.Name= memberUpdateDTO.Name;
            consulta.FacebookUrl = memberUpdateDTO.FacebookUrl;
            consulta.InstagramUrl = memberUpdateDTO.InstagramUrl;
            consulta.LinkedinUrl = memberUpdateDTO.LinkedinUrl;
            consulta.Description= memberUpdateDTO.Description;
            
            await _unitOfWork.MemberRepository.Update(consulta);
            await _unitOfWork.SaveChangesAsync();


            return new Result().Success("El miembro se ha cambiado correctamente");
            
        }
    }
}
