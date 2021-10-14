using OngProject.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces.IServices
{
    public interface ICommentsServices
    {
        Task<IEnumerable<CommentsDTO>> GetAll();
        Task<IEnumerable<CommentsDTO>> GetById(int id);
        bool EntityExists(int id);
        Task<NewCommentsDTO> Insert(NewCommentsDTO newCommentsDTO);
    }
}
