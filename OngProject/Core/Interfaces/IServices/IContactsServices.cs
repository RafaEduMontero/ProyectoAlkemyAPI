using OngProject.Common;
using OngProject.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces.IServices
{
    public interface IContactsServices
    {
        Task<Result> Insert(ContactInsertDTO contactInsertDTO);
        Task<IEnumerable<ContactDTO>> GetAll();
        Task<ContactDTO> GetById(int id);
        bool EntityExists(int id);
    }
}
