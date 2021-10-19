﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using OngProject.Core.Helper.Pagination;

namespace OngProject.Core.Interfaces.IServices
{
    public interface IMemberServices
    {
        Task<IEnumerable<MembersDTO>> GetAll();
        Task<Member> Insert(MembersInsertarDTO membersInsertarDTO);
        Task<Result> Delete(int id);
        Task<PaginationDTO<MembersDTO>> GetByPage(int page);
    }
}
