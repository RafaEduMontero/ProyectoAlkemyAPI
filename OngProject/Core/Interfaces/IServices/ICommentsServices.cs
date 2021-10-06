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
    }
}
