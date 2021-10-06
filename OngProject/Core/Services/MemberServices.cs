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
    public class MemberServices : IMemberServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public MemberServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MembersDTO>> GetAll()
        {
            var mapper = new EntityMapper();
            var Member = await _unitOfWork.MemberRepository.GetAll();
            var MemberDTO = Member.Select(x => mapper.FromMembersToMembersDto(x));
            return MemberDTO;
        }
    }
}
