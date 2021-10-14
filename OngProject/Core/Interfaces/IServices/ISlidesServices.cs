using OngProject.Common;
using OngProject.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces.IServices
{
    public interface ISlidesServices
    {
        Task<Result> Insert(SlidesCreateDTO slidesCreateDTO);
        Task<IEnumerable<SlidesDTO>> GetAll();
        Task<SlidesDTO> GetById(int id);
        bool EntityExist(int id);
    }
}
