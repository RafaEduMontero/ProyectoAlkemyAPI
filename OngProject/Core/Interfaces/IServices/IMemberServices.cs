using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;

namespace OngProject.Core.Interfaces.IServices
{
    public interface IMemberServices
    {
        Task<IEnumerable<MembersDTO>> GetAll();

        Task<Member> Insert(MembersDTO membersDTO);
    }
}
